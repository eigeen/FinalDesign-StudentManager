using LiveCharts;
using StudentManager.Access;
using StudentManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManager.ViewModels
{
    public class ChartsPageViewModel
    {
        public SqliteAccess db { get; set; }
        public SeriesCollection Series { get; set; }
        public ChartsPageViewModel()
        {
            db.Connect();
        }
        public List<ComboBoxElement> LoadClassComboBox()
        {
            List<SMCObject> classCol = db.FetchSMC(SMC.Classes);
            List<ComboBoxElement> cbCol = new List<ComboBoxElement> { };
            classCol.ForEach(item => cbCol.Add(
                new ComboBoxElement { ID = item.ID, Name = item.Name + "_" + item.ID }));
            return cbCol;
        }

        public void LoadChart()
        {
            
        }
    }
}
