using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateAgency.DataService;

namespace RealEstateAgency.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        IPersonDataService _personDataService;
        IEstateFileDataService _estateFileDataService;
        IPersonEventDataService _eventDataService;
        public ReportsController(IPersonDataService personDataService, 
                                 IEstateFileDataService estateFileDataService,
                                 IPersonEventDataService eventDataService)
        {
            _personDataService = personDataService;
            _estateFileDataService = estateFileDataService;
            _eventDataService = eventDataService;

        }
        [Control("Reports","read")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PersonnelPerformance(int userID, DateTime dateFrom, DateTime dateTo)
        {
            ViewBag.Charts = GetCharts(userID, dateFrom, dateTo);
            return PartialView();
        }
        public List<Highcharts> GetCharts(int userID, DateTime dateFrom, DateTime dateTo)
        {
            int maxValue = 100;
            var newCustomer = _personDataService.All().Count(p => p.CreateByID == userID && p.CreationDate >= dateFrom && p.CreationDate <= dateTo);
            var newFile = _estateFileDataService.All().Count(p => p.CreateByID == userID && p.CreateDate >= dateFrom && p.CreateDate <= dateTo);
            var callCustomer = _eventDataService.All().Count(e => e.EventType == "مشتری تلفنی" && e.CreateByID == userID && e.CreationDate >= dateFrom && e.CreationDate <= dateTo);
            var callOwner = _eventDataService.All().Count(e => e.EventType == "تماس با مالک" && e.CreateByID == userID && e.CreationDate >= dateFrom && e.CreationDate <= dateTo);
            var callCreator = _eventDataService.All().Count(e => e.EventType == "تماس با سازنده" && e.CreateByID == userID && e.CreationDate >= dateFrom && e.CreationDate <= dateTo);
            var callExpert = _eventDataService.All().Count(e => e.EventType == "کارشناسی" && e.CreateByID == userID && e.CreationDate >= dateFrom && e.CreationDate <= dateTo);
            string[] xAxisLabels = new string[] { "ثبت مشتری جدید", "ثبت فایل جدید", "تماس تلفنی با مشتری", "تماس تلفنی با مالکین", "تماس تلفنی با سازنده", "تعداد سرویس حضوری" };
            object[] yAxisDataActual = (new int[] { newCustomer, newFile, callCustomer, callOwner, callCreator, callExpert }).Cast<object>().ToArray();
            Highcharts performance = new Highcharts("PersonnelPerformanceChart")
              .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column, Style = "width:'100%'", Reflow = true })
              .SetTitle(new Title { Text = "گزارش عملکرد پرسنل", Style = "fontWeight: 'bold',fontFamily: 'B Yekan'" })
               .SetXAxis(new XAxis { Categories = xAxisLabels, Labels = new XAxisLabels { Rotation = 0, Style = "fontWeight: 'bold',fontFamily:'B Yekan'" } })
              .SetExporting(new Exporting
              {
                  Enabled = false
              })

              .SetPlotOptions(new PlotOptions { Bar = new PlotOptionsBar { Stacking = Stackings.Normal, DataLabels = new PlotOptionsBarDataLabels { Enabled = true, Inside = false } } })
              .SetCredits(new Credits { Enabled = false })
             .SetYAxis(new YAxis
             {
                 Min = 0,
                 Max = maxValue,
                 Title = new YAxisTitle { Text = "تعداد", Style = "fontWeight: 'bold',fontFamily: 'B Yekan'" },
                 Labels = new YAxisLabels
                 {

                     Enabled = true,
                     Style = "fontWeight: 'bold',fontFamily:'B Yekan'"
                 }
             })
              .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.x +': '+ this.y +''; }", UseHTML = true })

              .SetSeries(new[]
              {
                    new Series { Name = "عملکرد     ",
                        Data = new Data(yAxisDataActual),
                        Type= ChartTypes.Column ,
                        PlotOptionsColumn= new PlotOptionsColumn(){ Color = Color.Orange, 
                            DataLabels = new  PlotOptionsColumnDataLabels { Enabled = true,  Inside = false , Style = "fontWeight: 'bold',fontFamily:'Yekan'" ,UseHTML=true }}},


              });           
            return new List<Highcharts> { performance };
        }
    }
}