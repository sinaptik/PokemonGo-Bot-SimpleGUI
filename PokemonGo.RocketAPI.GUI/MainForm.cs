using PokemonGo.RocketAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Logic;
using PokemonGo.RocketAPI.GeneratedCode;
using AllEnum;
using PokemonGo.RocketAPI.Extensions;
using PokemonGo.RocketAPI.Logic.Utils;

namespace PokemonGo.RocketAPI.GUI
{
    public partial class MainForm : Form
    {
        Client client;
        Settings settings;
        Inventory inventory;
        GetPlayerResponse profile;

        bool isFarmingActive = false;

        public MainForm()
        {
            InitializeComponent();
            startLogger();
            cleanUp();
        }

        private void cleanUp()
        {
            // Clear Labels
            lbExpHour.Text = string.Empty;
            lbPkmnCaptured.Text = string.Empty;
            lbPkmnHr.Text = string.Empty;

            // Clear Labels
            lbName.Text = string.Empty;
            lbLevel.Text = string.Empty;
            lbExperience.Text = string.Empty;

            // Clear Experience
            totalExperience = 0;
            pokemonCaughtCount = 0;            
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                await displayLoginWindow();
                setProfileInformation();
                await GetCurrentLevelInformation();
                await preflightCheck();
            }
            catch
            {
            }
        }

        private async Task displayLoginWindow()
        {
            // Display Login
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Show();

            // Check if an Option was Selected
            if (!loginForm.loginSelected)
                Application.Exit();

            // Determine Login Method
            if (loginForm.auth == AuthType.Ptc)
                await loginPtc(loginForm.boxUsername.Text, loginForm.boxPassword.Text);
            if (loginForm.auth == AuthType.Google)
                await loginGoogle();

            // Close the Login Form
            loginForm.Close();
        }

        private void setProfileInformation()
        {
            lbName.Text = profile.Profile.Username ?? "N/A";
        }

        private void startLogger()
        {
            GUILogger GUILog = new GUILogger(LogLevel.Info);
            GUILog.setLoggingBox(loggingBox);
            Logger.SetLogger(GUILog);
        }

        private async Task loginGoogle()
        {
            try
            {
                // Creating the Settings
                Logger.Write("Adjusting the Settings.");
                UserSettings.Default.AuthType = AuthType.Google.ToString();
                this.settings = new Settings();

                // Begin Login
                Logger.Write("Trying to Login with Google Token...");
                Client client = new Client(this.settings);
                await client.DoGoogleLogin();
                await client.SetServer();

                // Server Ready
                Logger.Write("Connected! Server is Ready.");
                this.client = client;

                Logger.Write("Attempting to Retrieve Inventory and Player Profile...");
                this.inventory = new Inventory(client);
                this.profile = await client.GetProfile();
                enableButtons();
            }
            catch
            {
                Logger.Write("Unable to Connect using the Google Token.");
            }
        }

        private async Task loginPtc(string username, string password)
        {
            try
            {
                // Creating the Settings
                Logger.Write("Adjusting the Settings.");
                UserSettings.Default.AuthType = AuthType.Ptc.ToString();
                UserSettings.Default.PtcUsername = username;
                UserSettings.Default.PtcPassword = password;
                this.settings = new Settings();

                // Begin Login
                Logger.Write("Trying to Login with PTC Credentials...");
                Client client = new Client(this.settings);
                await client.DoPtcLogin(this.settings.PtcUsername, this.settings.PtcPassword);
                await client.SetServer();

                // Server Ready
                Logger.Write("Connected! Server is Ready.");
                this.client = client;

                Logger.Write("Attempting to Retrieve Inventory and Player Profile...");
                this.inventory = new Inventory(client);
                this.profile = await client.GetProfile();
                enableButtons();
            }         
            catch
            {
                Logger.Write("Unable to Connect using the PTC Credentials.");
            }
        }

        private void enableButtons()
        {
            btnStartFarming.Enabled = true;
            btnTransferDuplicates.Enabled = true;
            btnRecycleItems.Enabled = true;
            btnEvolvePokemons.Enabled = true;
            cbKeepPkToEvolve.Enabled = true;

            Logger.Write("Ready to Work.");
        }

        private async Task<bool> preflightCheck()
        {
            // Get Pokemons and Inventory
            var myItems = await inventory.GetItems();
            var myPokemons = await inventory.GetPokemons();            

            // Write to Console
            Logger.Write($"Items in Bag: {myItems.Select(i => i.Count).Sum()}/350.");
            Logger.Write($"Pokemons in Bag: {myPokemons.Count()}/250.");

            // Checker for Inventory
            if (myItems.Select(i => i.Count).Sum() >= 350)
            {
                Logger.Write("Unable to Start Farming: You need to have free space for Items.");
                return false;
            }

            // Checker for Pokemons
            if (myPokemons.Count() >= 241) // Eggs are Included in the total count (9/9)
            {
                Logger.Write("Unable to Start Farming: You need to have free space for Pokemons.");
                return false;
            }

            // Ready to Fly
            Logger.Write("Inventory and Pokemon Space, Ready.");
            return true;
        }

        ///////////////////
        // Buttons Logic //
        ///////////////////

        private async void btnStartFarming_Click(object sender, EventArgs e)
        {
            if (!await preflightCheck())
                return;

            // Disable Button
            btnStartFarming.Enabled = false;
            btnEvolvePokemons.Enabled = false;
            btnRecycleItems.Enabled = false;
            btnTransferDuplicates.Enabled = false;
            cbKeepPkToEvolve.Enabled = false;
            lbCanEvolveCont.Enabled = false;


            btnStopFarming.Enabled = true;

            // Setup the Timer
            isFarmingActive = true;
            setUpTimer();
            startBottingSession();

            // Clear Grid
            dGrid.Rows.Clear();

            // Prepare Grid
            dGrid.ColumnCount = 3;
            dGrid.Columns[0].Name = "Action";
            dGrid.Columns[1].Name = "Pokemon";
            dGrid.Columns[2].Name = "CP";
        }

        private void btnStopFarming_Click(object sender, EventArgs e)
        {
            // Disable Button
            btnStartFarming.Enabled = true;
            btnEvolvePokemons.Enabled = true;
            btnRecycleItems.Enabled = true;
            btnTransferDuplicates.Enabled = true;
            cbKeepPkToEvolve.Enabled = true;
            lbCanEvolveCont.Enabled = true;

            btnStopFarming.Enabled = false;

            // Close the Timer
            isFarmingActive = false;
            stopBottingSession();
        }

        private async void btnEvolvePokemons_Click(object sender, EventArgs e)
        {
            // Clear Grid
            dGrid.Rows.Clear();

            // Prepare Grid
            dGrid.ColumnCount = 3;
            dGrid.Columns[0].Name = "Action";
            dGrid.Columns[1].Name = "Pokemon";
            dGrid.Columns[2].Name = "Experience";

            // Evolve Pokemons
            await EvolveAllPokemonWithEnoughCandy();
        }

        private async void btnTransferDuplicates_Click(object sender, EventArgs e)
        {
            // Clear Grid
            dGrid.Rows.Clear();

            // Prepare Grid
            dGrid.ColumnCount = 3;
            dGrid.Columns[0].Name = "Action";
            dGrid.Columns[1].Name = "Pokemon";
            dGrid.Columns[2].Name = "CP";

            // Transfer Pokemons
            await TransferDuplicatePokemon(cbKeepPkToEvolve.Checked);
        }

        private async void btnRecycleItems_Click(object sender, EventArgs e)
        { 
            // Clear Grid
            dGrid.Rows.Clear();

            // Prepare Grid
            dGrid.ColumnCount = 3;
            dGrid.Columns[0].Name = "Action";
            dGrid.Columns[1].Name = "Count";
            dGrid.Columns[2].Name = "Item";

            // Recycle Items
            await RecycleItems();
        }

        ////////////////////////
        // EXP COUNTER MODULE //
        ////////////////////////

        double totalExperience = 0.0;
        int pokemonCaughtCount = 0;
        int pokestopsCount = 0;
        DateTime sessionStartTime;
        Timer sessionTimer = new Timer();

        private void setUpTimer()
        {
            sessionTimer.Tick += new EventHandler(timerTick);
            sessionTimer.Enabled = true;
        }

        private void timerTick(object sender, EventArgs e)
        {
            lbExpHour.Text = getExpPerHour();
            lbPkmnHr.Text = getPokemonPerHour();
            lbPkmnCaptured.Text = "Pokemons Captured: " + pokemonCaughtCount.ToString();
        }

        private string getExpPerHour()
        {
            double expPerHour = (totalExperience * 3600) / (DateTime.Now - sessionStartTime).TotalSeconds;
            return $"Exp/Hr: {expPerHour:0.0}";
        }

        private string getPokemonPerHour()
        {
            double pkmnPerHour = (pokemonCaughtCount * 3600) / (DateTime.Now - sessionStartTime).TotalSeconds;
            return $"Pkmn/Hr: {pkmnPerHour:0.0}";
        }

        private async void startBottingSession()
        {
            // Setup the Timer
            sessionTimer.Interval = 5000;
            sessionTimer.Start();
            sessionStartTime = DateTime.Now;

            try {
                // Start Farming Pokestops/Pokemons.
                await ExecuteFarmingPokestopsAndPokemons();
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
                btnStopFarming_Click(null, null);
                Logger.Write("Stopping the Bot, a problem was found.");
            }
        }

        private void stopBottingSession()
        {
            sessionTimer.Stop();

            boxPokestopName.Clear();
            boxPokestopInit.Clear();
            boxPokestopCount.Clear();

            MessageBox.Show("Please allow a few seconds for the pending tasks to complete.");
        }

        ///////////////////////
        // API LOGIC MODULES //
        ///////////////////////

        // Credits to Spegeli aka. MaxMuster for this function
        public async Task GetCurrentLevelInformation()
        {
            var inventory = await client.GetInventory();
            var stats = inventory.InventoryDelta.InventoryItems.Select(i => i.InventoryItemData?.PlayerStats).ToArray();
            foreach (var v in stats)
                if (v != null)
                {
                    lbLevel.Text = $"Level {v.Level}";
                    lbExperience.Text = $"{v.Experience} / {v.NextLevelXp}";
                }
        }

        private async Task EvolveAllPokemonWithEnoughCandy()
        {
            // Logging
            Logger.Write("Selecting Pokemons available for Evolution.");

            var pokemonToEvolve = await inventory.GetPokemonToEvolve();
            foreach (var pokemon in pokemonToEvolve)
            {
                var evolvePokemonOutProto = await client.EvolvePokemon((ulong)pokemon.Id);

                if (evolvePokemonOutProto.Result == EvolvePokemonOut.Types.EvolvePokemonStatus.PokemonEvolvedSuccess)
                {
                    Logger.Write($"Evolved {pokemon.PokemonId} successfully for {evolvePokemonOutProto.ExpAwarded}xp", LogLevel.Info);

                    // GUI Experience
                    totalExperience += evolvePokemonOutProto.ExpAwarded;
                    dGrid.Rows.Add(new string[] { "Evolved", pokemon.PokemonId.ToString(), evolvePokemonOutProto.ExpAwarded.ToString() });
                }                    
                else
                {
                    Logger.Write($"Failed to evolve {pokemon.PokemonId}. EvolvePokemonOutProto.Result was {evolvePokemonOutProto.Result}, stopping evolving {pokemon.PokemonId}", LogLevel.Info);
                }

                await GetCurrentLevelInformation();
                await Task.Delay(3000);
            }

            // Logging
            Logger.Write("Finished Evolving Pokemons.");
        }

        private async Task TransferDuplicatePokemon(bool keepPokemonsThatCanEvolve = false)
        {
            // Logging
            Logger.Write("Selecting Pokemons available for Transfer.");

            var duplicatePokemons = await inventory.GetDuplicatePokemonToTransfer(keepPokemonsThatCanEvolve);

            foreach (var duplicatePokemon in duplicatePokemons)
            {
                var transfer = await client.TransferPokemon(duplicatePokemon.Id);
                Logger.Write($"Transfer {duplicatePokemon.PokemonId} with {duplicatePokemon.Cp} CP", LogLevel.Info);

                // GUI Experience
                dGrid.Rows.Add(new string[] { "Transferred", duplicatePokemon.PokemonId.ToString(), duplicatePokemon.Cp.ToString() });

                await GetCurrentLevelInformation();
                await Task.Delay(500);
            }

            // Logging
            Logger.Write("Finished Transfering Pokemons.");
        }

        private async Task RecycleItems()
        {
            // Logging
            Logger.Write("Recycling Items to Free Space");

            var items = await inventory.GetItemsToRecycle(this.settings);

            foreach (var item in items)
            {
                var transfer = await client.RecycleItem((AllEnum.ItemId)item.Item_, item.Count);
                Logger.Write($"Recycled {item.Count}x {(AllEnum.ItemId)item.Item_}", LogLevel.Info);

                // GUI Experience
                dGrid.Rows.Add(new string[] { "Recycled", item.Count.ToString(), ((AllEnum.ItemId)item.Item_).ToString() });

                await Task.Delay(500);
            }

            // Logging
            Logger.Write("Recycling Complete.");
        }

        private async Task<MiscEnums.Item> GetBestBall(int? pokemonCp)
        {
            var pokeBallsCount = await inventory.GetItemAmountByType(MiscEnums.Item.ITEM_POKE_BALL);
            var greatBallsCount = await inventory.GetItemAmountByType(MiscEnums.Item.ITEM_GREAT_BALL);
            var ultraBallsCount = await inventory.GetItemAmountByType(MiscEnums.Item.ITEM_ULTRA_BALL);
            var masterBallsCount = await inventory.GetItemAmountByType(MiscEnums.Item.ITEM_MASTER_BALL);

            if (masterBallsCount > 0 && pokemonCp >= 1000)
                return MiscEnums.Item.ITEM_MASTER_BALL;
            else if (ultraBallsCount > 0 && pokemonCp >= 1000)
                return MiscEnums.Item.ITEM_ULTRA_BALL;
            else if (greatBallsCount > 0 && pokemonCp >= 1000)
                return MiscEnums.Item.ITEM_GREAT_BALL;

            if (ultraBallsCount > 0 && pokemonCp >= 600)
                return MiscEnums.Item.ITEM_ULTRA_BALL;
            else if (greatBallsCount > 0 && pokemonCp >= 600)
                return MiscEnums.Item.ITEM_GREAT_BALL;

            if (greatBallsCount > 0 && pokemonCp >= 350)
                return MiscEnums.Item.ITEM_GREAT_BALL;

            if (pokeBallsCount > 0)
                return MiscEnums.Item.ITEM_POKE_BALL;
            if (greatBallsCount > 0)
                return MiscEnums.Item.ITEM_GREAT_BALL;
            if (ultraBallsCount > 0)
                return MiscEnums.Item.ITEM_ULTRA_BALL;
            if (masterBallsCount > 0)
                return MiscEnums.Item.ITEM_MASTER_BALL;

            return MiscEnums.Item.ITEM_POKE_BALL;
        }

        public async Task UseBerry(ulong encounterId, string spawnPointId)
        {
            var inventoryBalls = await inventory.GetItems();
            var berries = inventoryBalls.Where(p => (ItemId)p.Item_ == ItemId.ItemRazzBerry);
            var berry = berries.FirstOrDefault();

            if (berry == null)
                return;

            var useRaspberry = await client.UseCaptureItem(encounterId, AllEnum.ItemId.ItemRazzBerry, spawnPointId);
            Logger.Write($"Used Rasperry. Remaining: {berry.Count}", LogLevel.Info);
            await Task.Delay(3000);
        }

        private async Task ExecuteFarmingPokestopsAndPokemons()
        {
            var mapObjects = await client.GetMapObjects();

            var pokeStops = mapObjects.MapCells.SelectMany(i => i.Forts).Where(i => i.Type == FortType.Checkpoint && i.CooldownCompleteTimestampMs < DateTime.UtcNow.ToUnixTime());

            pokestopsCount = pokeStops.Count<FortData>();
            int count = 1;

            foreach (var pokeStop in pokeStops)
            {
                var update = await client.UpdatePlayerLocation(pokeStop.Latitude, pokeStop.Longitude, settings.DefaultAltitude);
                var fortInfo = await client.GetFort(pokeStop.Id, pokeStop.Latitude, pokeStop.Longitude);

                boxPokestopName.Text = fortInfo.Name.ToString();
                boxPokestopInit.Text = count.ToString();
                boxPokestopCount.Text = pokestopsCount.ToString();
                count++;                               

                var fortSearch = await client.SearchFort(pokeStop.Id, pokeStop.Latitude, pokeStop.Longitude);

                Logger.Write($"Loot -> Gems: { fortSearch.GemsAwarded}, Eggs: {fortSearch.PokemonDataEgg} Items: {StringUtils.GetSummedFriendlyNameOfItemAwardList(fortSearch.ItemsAwarded)}", LogLevel.Info);
                Logger.Write("Gained " + fortSearch.ExperienceAwarded + " XP.");

                // Experience Counter
                totalExperience += fortSearch.ExperienceAwarded;

                await GetCurrentLevelInformation();
                Logger.Write("Attempting to Capture Nearby Pokemons.");
                await ExecuteCatchAllNearbyPokemons();

                if (!isFarmingActive)
                {
                    Logger.Write("Stopping Farming Pokestops.");
                    return;
                }                    

                Logger.Write("Waiting 10 seconds before moving to the next Pokestop.");
                await Task.Delay(10000);
            }
        }

        private async Task ExecuteCatchAllNearbyPokemons()
        {
            var mapObjects = await client.GetMapObjects();

            var pokemons = mapObjects.MapCells.SelectMany(i => i.CatchablePokemons);

            Logger.Write("Found " + pokemons.Count<MapPokemon>() + " Pokemons in the area.");
            foreach (var pokemon in pokemons)
            {   
                var update = await client.UpdatePlayerLocation(pokemon.Latitude, pokemon.Longitude, settings.DefaultAltitude);
                var encounterPokemonResponse = await client.EncounterPokemon(pokemon.EncounterId, pokemon.SpawnpointId);
                var pokemonCP = encounterPokemonResponse?.WildPokemon?.PokemonData?.Cp;
                var pokeball = await GetBestBall(pokemonCP);

                Logger.Write($"Fighting {pokemon.PokemonId} with Capture Probability of {(encounterPokemonResponse?.CaptureProbability.CaptureProbability_.First())*100:0.0}%");

                boxPokemonName.Text = pokemon.PokemonId.ToString();
                boxPokemonCaughtProb.Text = (encounterPokemonResponse?.CaptureProbability.CaptureProbability_.First() * 100).ToString() + "%";                

                CatchPokemonResponse caughtPokemonResponse;
                do
                {
                    if (encounterPokemonResponse?.CaptureProbability.CaptureProbability_.First() < 0.4)
                    {
                        //Throw berry is we can
                        await UseBerry(pokemon.EncounterId, pokemon.SpawnpointId);
                    }

                    caughtPokemonResponse = await client.CatchPokemon(pokemon.EncounterId, pokemon.SpawnpointId, pokemon.Latitude, pokemon.Longitude, pokeball);
                    await Task.Delay(2000);
                }
                while (caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchMissed);

                Logger.Write(caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchSuccess ? $"We caught a {pokemon.PokemonId} with CP {encounterPokemonResponse?.WildPokemon?.PokemonData?.Cp} using a {pokeball}" : $"{pokemon.PokemonId} with CP {encounterPokemonResponse?.WildPokemon?.PokemonData?.Cp} got away while using a {pokeball}..", LogLevel.Info);

                if (caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchSuccess)
                {
                    // Calculate Experience
                    int fightExperience = 0;
                    foreach (int exp in caughtPokemonResponse.Scores.Xp)
                        fightExperience += exp;
                    totalExperience += fightExperience;
                    Logger.Write("Gained " + fightExperience + " XP.");
                    pokemonCaughtCount++;

                    // GUI Experience
                    dGrid.Rows.Add(new string[] {
                        "Captured",
                        pokemon.PokemonId.ToString(),
                        encounterPokemonResponse?.WildPokemon?.PokemonData?.Cp.ToString()
                    });
                }
                else
                {
                    // GUI Experience
                    dGrid.Rows.Add(new string[] {
                        "Ran Away",
                        pokemon.PokemonId.ToString(),
                        encounterPokemonResponse?.WildPokemon?.PokemonData?.Cp.ToString()
                    });
                }

                boxPokemonName.Clear();
                boxPokemonCaughtProb.Clear();

                await GetCurrentLevelInformation();

                if (!isFarmingActive)
                {
                    Logger.Write("Stopping Farming Pokemons.");
                    return;
                }

                Logger.Write("Waiting 10 seconds before moving to the next Pokemon.");
                await Task.Delay(10000);
            }
        }


    }
}
