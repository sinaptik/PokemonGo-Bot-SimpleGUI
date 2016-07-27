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
using System.Net;
using System.IO;

namespace PokemonGo.RocketAPI.GUI
{
    public partial class PokemonForm : Form
    {
        Client client;

        public PokemonForm(Client client)
        {
            this.client = client;
            InitializeComponent();
        }

        private async void PokemonForm_Load(object sender, EventArgs e)
        {
            await Execute();
        }    
        private async Task Execute()
        {            
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
                imageList.Images.Add(pokemon.PokemonId.ToString(), await GetPokemonImageAsync((int)pokemon.PokemonId));

                pokemonListView.LargeImageList = imageList;
                var listViewItem = new ListViewItem();
                listViewItem.SubItems.Add("Cp: " + pokemon.Cp);

                var currentCandy = families
                    .Where(i => (int)i.FamilyId <= (int)pokemon.PokemonId)
                    .Select(f => f.Candy)
                    .First();
                
                listViewItem.ImageKey = pokemon.PokemonId.ToString();
                var pokemonIv = Math.Floor(Logic.Logic.CalculatePokemonPerfection(pokemon));
                listViewItem.Text = string.Format("{0}\nCP {1} IV {2}", pokemon.PokemonId, pokemon.Cp, pokemonIv);
                listViewItem.ToolTipText = "Candy: " + currentCandy;

                pokemonListView.Items.Add(listViewItem);
            }
        }

        private async Task<Bitmap> GetPokemonImageAsync(int pokemonId)
        {
            WebRequest req = WebRequest.Create("http://pokeapi.co/media/sprites/pokemon/"+pokemonId+".png");
            WebResponse res = await req.GetResponseAsync();
            Stream resStream = res.GetResponseStream();
            return new Bitmap(resStream);
        }
    }
}
