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
            Add(new LetterInfo { Letter = 'q', Count = 5 });
            Add(new LetterInfo { Letter = 'z', Count = 2 });
            Add(new LetterInfo { Letter = 'w', Count = 9 });
        }

        #region Methods
        public void FillLetterList(string str)
        {
            var query = str.ToLower().Replace(" ", "").GroupBy(c => c)
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
