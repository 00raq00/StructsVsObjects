﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructsVsObjects
{
    public struct SampleStruct
    {
       internal int i;
       internal int j;
        internal int z;

        public SampleStruct(int i, int j, int z)
        {
            this.i = i;
            this.j = j;
            this.z = z;
        }
    }
}
