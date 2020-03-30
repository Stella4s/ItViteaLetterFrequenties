using ItViteaLetterFrequenties.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItViteaLetterFrequenties
{
    public class LetterInfoList : ObservableCollection<LetterInfo>
    {
        public LetterInfoList()
        {
        }

        #region Methods
        public void FillLetterList(string str)
        {
            var query = str.ToLower().GroupBy(c => c)
                    .Select(g => new { Letter = g.Key, Count = g.Count() })
                    .OrderBy(c => c.Letter).ToList();
           
            Clear();
            foreach (var item in query)
            {
                Add(new LetterInfo { Letter = item.Letter, Count = item.Count });
            }
        }
        #endregion

    }
}
