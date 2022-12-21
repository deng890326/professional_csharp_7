using System;
using System.Collections.Generic;
using System.Text;

namespace Framewok
{
    public interface ICopyOperation<in TDest>
    {
        void CopyTo(TDest dest);
    }
}
