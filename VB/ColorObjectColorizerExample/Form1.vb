Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Xml.Linq
Imports DevExpress.XtraCharts

Namespace ColorObjectColorizerExample
    Partial Public Class Form1
        Inherits Form

        Private Const filepath As String = "..\..\Data\GDP.xml"

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
'            #Region "#BarSeries"
            ' Create and customize a bar series.
            Dim barSeries As New Series() With {.DataSource = LoadData(filepath), .Colorizer = New ColorObjectColorizer(), .ArgumentDataMember = "Country", .ColorDataMember = "NationalColor", .View = New SideBySideBarSeriesView()}
            barSeries.ValueDataMembers.AddRange(New String() { "Product" })
'            #End Region ' #BarSeries

            ' Add the series to the ChartControl's Series collection.
            chartControl1.Series.Add(barSeries)

            ' Show a title for the values axis.
            CType(chartControl1.Diagram, XYDiagram).AxisY.Title.Text = "GDP per capita, $"
            CType(chartControl1.Diagram, XYDiagram).AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
        End Sub

        #Region "#DataLoad"
        Private Class HpiPoint
            Public Property Country() As String
            Public Property Product() As Double
            Public Property NationalColor() As String
        End Class

        ' Loads data from an XML data source.
        Private Shared Function LoadData(ByVal filepath As String) As List(Of HpiPoint)
            Dim doc As XDocument = XDocument.Load(filepath)
            Dim points As New List(Of HpiPoint)()
            For Each element As XElement In doc.Element("G20HPIs").Elements("CountryStatistics")
                points.Add(New HpiPoint() With {.Country = element.Element("Country").Value, .Product = Convert.ToDouble(element.Element("Product").Value), .NationalColor = element.Element("NationalColor").Value})
            Next element
            Return points
        End Function
        #End Region ' #DataLoad
    End Class
End Namespace
