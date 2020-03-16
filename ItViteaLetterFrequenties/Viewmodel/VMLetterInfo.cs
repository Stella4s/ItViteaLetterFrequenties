using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using ItViteaLetterFrequenties.Model;

namespace ItViteaLetterFrequenties.Viewmodel
{
    class VMLetterInfo
    {
        public VMLetterInfo()
        {
            LetterList = new List<LetterInfo>();

        }

        public IList<LetterInfo> LetterList { get; set; }

        public ICommand NewCommand { private set; get; }


        public void FillLetterList(string str)
        {
            var query = str.ToLower().Replace(" ", "").GroupBy(c => c)
                    .Select(g => new { Letter = g.Key, Count = g.Count() })
                    .OrderBy(c => c.Letter).ToList();

            foreach (var item in query)
            {
                LetterList.Add(new LetterInfo { Letter = item.Letter, Count = item.Count });
            }
        }

    }
}
