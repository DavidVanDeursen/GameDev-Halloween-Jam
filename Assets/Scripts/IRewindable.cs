using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal interface IRewindable
    {
        public void StartRewind();
        IEnumerator Rewind();
    }

