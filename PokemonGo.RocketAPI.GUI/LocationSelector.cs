using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonGo.RocketAPI.GUI
{   
    public partial class LocationSelector : Form
    {
        public class Loc
        {
            public string name { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }

            public override string ToString()
            {
                return name;
            }
        }

        public double lat;
        public double lng;

        public LocationSelector()
        {
            InitializeComponent();
        }

        private void addGoodFarmingLocations()
        {
            // Good Farming Locations (Create Issue in GutHub to Add More)
            comboLocations.Items.Add(new Loc() { name = "London, England", lat = 51.501663, lng = -0.14102 });
            comboLocations.Items.Add(new Loc() { name = "Santa Monica Pier, LA, USA", lat = 34.00873594425199, lng = -118.49761247634888 });
            comboLocations.Items.Add(new Loc() { name = "Bryant Park, NY, USA", lat = 40.75320648472645, lng = -73.98390769958496 });
            comboLocations.Items.Add(new Loc() { name = "Sydney, Australia", lat = -33.870273020353416, lng = 151.20878219604492 });
            comboLocations.Items.Add(new Loc() { name = "Tokyo, Japan", lat = 35.69051125265253, lng = 139.68954205513 });
        }        

        private void LocationSelector_Load(object sender, EventArgs e)
        {            
            // Create the Map
            initializeMap();

            // Add Options
            addGoodFarmingLocations();
        }

        private void initializeMap()
        {
            try
            {
                // Load the Map Settings
                MainMap.OnMapDrag += MainMap_OnMapDrag;
                MainMap.DragButton = MouseButtons.Left;
                MainMap.MapProvider = GMapProviders.BingMap;                
                MainMap.Position = new GMap.NET.PointLatLng(UserSettings.Default.DefaultLatitude, UserSettings.Default.DefaultLongitude);
                MainMap.MinZoom = 0;
                MainMap.MaxZoom = 24;
                MainMap.Zoom = 15;

                // Set the Initial Position
                boxLat.Text = UserSettings.Default.DefaultLatitude.ToString("0.000000");
                boxLng.Text = UserSettings.Default.DefaultLongitude.ToString("0.000000");

                // Assign Variables
                lat = UserSettings.Default.DefaultLatitude;
                lng = UserSettings.Default.DefaultLongitude;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void MainMap_OnMapDrag()
        {
            // Update Position
            lat = MainMap.Position.Lat;
            lng = MainMap.Position.Lng;

            // Update the TextBoxes
            boxLat.Text = lat.ToString("0.000000");
            boxLng.Text = lng.ToString("0.000000");
        }

        private void btnSetLocation_Click(object sender, EventArgs e)
        {
            // Persist the Position
            UserSettings.Default.DefaultLatitude = lat;
            UserSettings.Default.DefaultLongitude = lng;
            UserSettings.Default.Save();

            // Close this Window
            this.Hide();
        }

        private void comboLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the Item Selected
            Loc location = (Loc)comboLocations.SelectedItem;
            boxLat.Text = location.lat.ToString();
            boxLng.Text = location.lng.ToString();

            // Store the Variables
            lat = location.lat;
            lng = location.lng;

            // Update the Map
            MainMap.Position = new GMap.NET.PointLatLng(lat, lng);
        }

        private void MainMap_Load(object sender, EventArgs e)
        {

        }
    }
}
