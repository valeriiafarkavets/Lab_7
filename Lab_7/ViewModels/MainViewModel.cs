using System.Collections.ObjectModel;
using Lab_7.Models;
using Lab_7.Services;
namespace Lab_7.ViewModels{
    public class MainViewModel{
        private DateParserService parser = new DateParserService();
        public string InputText { get; set; } = "";
        public ObservableCollection<DateResult> Results { get; set; }  = new ObservableCollection<DateResult>();
        public int TotalCount{
            get { return Results.Count; }
        }
        public int RecognizedCount{
            get{
                int count = 0;
                foreach (DateResult result in Results) {
                    if (result.IsRecognized)
                        count++;
                }

                return count;
            }
        }
        public void Analyze(){
            Results.Clear();
            foreach (DateResult result in parser.ParseText(InputText)){
                Results.Add(result);
            }
        }
        public void Clear() {
            InputText = "";
            Results.Clear();
        }
    }
}
