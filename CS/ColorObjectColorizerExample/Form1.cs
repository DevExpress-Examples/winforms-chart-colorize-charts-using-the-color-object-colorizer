using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.XtraCharts;

namespace ColorObjectColorizerExample {
    public partial class Form1 : Form {
        const string filepath = "..\\..\\Data\\GDP.xml";
        
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            #region #BarSeries
            // Create and customize a bar series.
            Series barSeries = new Series() {
                DataSource = LoadData(filepath),
                Colorizer = new ColorObjectColorizer(),
                ArgumentDataMember = "Country",
                ColorDataMember = "NationalColor",
                View = new SideBySideBarSeriesView()
            };
            barSeries.ValueDataMembers.AddRange(new string[] { "Product" });
            #endregion #BarSeries

            // Add the series to the ChartControl's Series collection.
            chartControl1.Series.Add(barSeries);

            // Show a title for the values axis.
            ((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "GDP per capita, $";
            ((XYDiagram)chartControl1.Diagram).AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        }
        
        #region #DataLoad
        class HpiPoint {
            public string Country { get; set; }
            public double Product { get; set; }
            public string NationalColor { get; set; }
        }

        // Loads data from an XML data source.
        static List<HpiPoint> LoadData(string filepath) {
            XDocument doc = XDocument.Load(filepath);
            List<HpiPoint> points = new List<HpiPoint>();
            foreach (XElement element in doc.Element("G20HPIs").Elements("CountryStatistics")) {
                points.Add(new HpiPoint() {
                    Country = element.Element("Country").Value,
                    Product = Convert.ToDouble(element.Element("Product").Value),
                    NationalColor = element.Element("NationalColor").Value
                });
            }
            return points;
        }
        #endregion #DataLoad
    }
}
