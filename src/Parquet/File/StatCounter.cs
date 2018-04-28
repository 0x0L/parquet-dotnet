using System.Collections;

namespace Parquet.File
{
    class StatCounter
    {
        readonly IList _list;

        public StatCounter(IList list)
        {
            _list = list;

            //todo: calculate stats
        }
    }
}
