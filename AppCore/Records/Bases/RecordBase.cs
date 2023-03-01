using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Records.Bases
{
    //ilişkili entityler dışındaki tüm entitylerin miras alacağı ana class'tır.
    public abstract class RecordBase
    {
        public int Id { get; set; }
    }
}
