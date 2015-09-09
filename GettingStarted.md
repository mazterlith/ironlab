# Requirements #
IronLab runs on .NET 4.0.
Some features (e.g. thick 3D lines) require the  [DirectX End-User Runtime](http://www.microsoft.com/en-us/download/details.aspx?id=35). IronLab is only tested against Windows 7, although has been run successfully on other systems.

IronLab is built using Visual Studio 2010.

There is a released set of binaries, although the recommendation is to obtain the latest version from SVN. Concerning the released zip files, IronPlot includes only the IronPython plotting component that can be unzipped and copied into the IronPython site-packages folder. IronLab includes IronPlot, the IronPythonConsole and is packaged with NumPy. The idea is to give a demonstration of how a complete embeddable console can be assembled from the components.

**When downloading using Internet Explorer, ensure the zip file is unblocked prior to unzipping (right click zip file -> Properties -> General -> Unblock, then unzip).**

# Build from source #
## Basic build ##
  1. Download complete source code from Trunk.
  1. Visual Studio 2010 solution file and build all.
  1. Set IronPythonConsole as start-up project and run.
  1. PlotSample gives some examples of using the IronPlot library from C# rather than from IronPython. Try running .py files in Examples. Only non-NumPy examples will work without NumPy of course.

## Adding NumPy and SciPy ##
An example configuration is provided in the IronLab zip file.

The easiest way to get started is from an existing IronPython installation that includes NumPy and SciPy, copying the required packages.
Copy your IronPython 2.7 installation folders (including NumPy and SciPy)
**DLLs, Doc, Lib** into the build bin directory.
Copy ILabPythonLib\ironplot folder into the newly copied bin\Lib\site-packages.
Try:
```py

import numpy as np
import ironplot as ip
ip.plot(np.sin(np.arange(0, 10, 0.1)))
```
to check this is working. Then look at Examples for more examples.