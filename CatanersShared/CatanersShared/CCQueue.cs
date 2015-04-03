using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace CatanersShared
{
    public class CCQueue : ConcurrentQueue<String>
    {

        public void push(String item) {
            processMessage();
            base.Enqueue(item);
        }

        public async void processMessage()
        {
            // TODO Process List;
        }
    }
}
