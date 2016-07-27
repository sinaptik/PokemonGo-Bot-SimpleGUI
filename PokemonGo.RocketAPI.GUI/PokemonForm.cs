using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PokemonGo.RocketAPI.Enums;

namespace PokemonGo.RocketAPI.GUI
{
    public partial class PokemonForm : Form
    {
        Settings settings;
        public PokemonForm()
        {
            InitializeComponent();
            this.settings = new Settings();
            Execute();
        }

        // Taken from my old version from on a different source
        // which as been updated by a user.
        private async void Execute()
        {
            var client = new Client(this.settings);

            switch (settings.AuthType)
            {
                case AuthType.Ptc:
                    await client.DoPtcLogin(settings.PtcUsername, settings.PtcPassword);
                    break;
                case AuthType.Google:
                    await client.DoGoogleLogin();
                    break;
            }
            await client.SetServer();
            var inventory = await client.GetInventory();
            var pokemons =
                inventory.InventoryDelta.InventoryItems
                .Select(i => i.InventoryItemData?.Pokemon)
                    .Where(p => p != null && p?.PokemonId > 0)
                    .OrderByDescending(key => key.Cp);

            var families = inventory.InventoryDelta.InventoryItems
                .Select(i => i.InventoryItemData?.PokemonFamily)
                .Where(p => p != null && (int)p?.FamilyId > 0)
                .OrderByDescending(p => (int)p.FamilyId);

            var imageList = new ImageList { ImageSize = new Size(50, 50) };
            pokemonListView.ShowItemToolTips = true;

            foreach (var pokemon in pokemons)
            {

                var pokemonImage = GetPokemonImage((int)pokemon.PokemonId);
                imageList.Images.Add(pokemon.PokemonId.ToString(), pokemonImage);

                pokemonListView.LargeImageList = imageList;
                var listViewItem = new ListViewItem();
                listViewItem.SubItems.Add("Cp: " + pokemon.Cp);


                var currentCandy = families
                    .Where(i => (int)i.FamilyId <= (int)pokemon.PokemonId)
                    .Select(f => f.Candy)
                    .First();

                //listViewItem.SubItems.Add();
                listViewItem.ImageKey = pokemon.PokemonId.ToString();
                listViewItem.Text = string.Format("{0}\nCp: {1}", pokemon.PokemonId, pokemon.Cp);
                listViewItem.ToolTipText = "Candy: " + currentCandy;


                this.pokemonListView.Items.Add(listViewItem);
            }
        }

        // Again, not my methods but it looks good so why not :+1:
        private static Bitmap GetPokemonImage(int pokemonId)
        {
            var url = "http://pokeapi.co/media/sprites/pokemon/" + pokemonId + ".png";
            PictureBox picbox = new PictureBox();
            picbox.Load(url);
            Bitmap bitmapRemote = (Bitmap)picbox.Image;
            return bitmapRemote;
        }
    }
}
