using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace кэ4
{
   public partial class Zayavka
    {
        public override string ToString()
        {
            return "Номер заявки: " + ZayavkaNumber.ToString().Trim() + ", Дата:  "+ ZayavkaDate + ", Описание: " + ZayavkaDescription.Trim() + ", Комментарий: " +  ZayavkaComment.Trim() ;
        }
    }

    public partial class Artists
    {
        public override string ToString()
        {
            return ArtistName.Trim() ;
        }
    }
    public partial class Clients
    {
        public override string ToString()
        {
            return ClientName.Trim();
        }
    }
    public partial class Oboryd
    {
        public override string ToString()
        {
            return OborydName.Trim();
        }
    }
    public partial class Status
    {
        public override string ToString()
        {
            return StatusName.Trim();
        }
    }
    public partial class Type
    {
        public override string ToString()
        {
            return TypeName.Trim();
        }
    }
}
