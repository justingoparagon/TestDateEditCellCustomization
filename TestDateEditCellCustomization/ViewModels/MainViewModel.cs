using DevExpress.Mvvm.CodeGenerators;
using DevExpress.Xpf.Editors.DateNavigator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media;

namespace TestDateEditCellCustomization.ViewModels
{
    [GenerateViewModel]
    public partial class MainViewModel
    {
        [GenerateProperty]
        // this could be a property of all dates with notes, to have them appear in bold.
        List<DateTime> _Dates;

        [GenerateProperty]
        List<TblDateNote> _DateNotes;

        public MainViewModel()
        {
            #region Get test data
            Dates = new List<DateTime>()
            {
                DateTime.Today.AddDays(2),
                DateTime.Today.AddDays(4),
                DateTime.Today.AddDays(6),
                DateTime.Today.AddDays(8),
            };

            DateNotes = new List<TblDateNote>();

            for (int i = 0; i < Dates.Count; i++)
            {
                var date = Dates[i];
                DateNotes.Add(new TblDateNote() { Background = i % 2 == 0 ? "#fc9595" : "#959ffc", Date = date, Note = i % 2 == 0 ? "Red note" : "Blue note" });
            }
            #endregion
        }

        [GenerateCommand]
        public void RequestCellAppearance(DateNavigatorRequestCellAppearanceEventArgs e)
        {
            var dayNote = DateNotes.FirstOrDefault(w => w.Date == e.DateTime);

            if (dayNote != null)
            {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString(dayNote.Background);
                e.Appearance.Background = brush;
            }
        }
    }

    public class TblDateNote
    {
        public DateTime Date { get; set; }
        public string Background { get; set; }
        public string Note { get; set; }
    }
}
