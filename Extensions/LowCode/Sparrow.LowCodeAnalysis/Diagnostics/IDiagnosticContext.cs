﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    public interface IDiagnosticContext
    {
        Project Project { get; }
        Document? Document { get; }
    }
}
