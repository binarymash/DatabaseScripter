﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Bluejam.Utils.DatabaseScripter")]
[assembly: AssemblyDescription("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("720df19f-bd5d-4771-9448-a62c37ccc3c5")]

// Configure log4net using the .config file
// This will cause log4net to look for a configuration file in the application 
// base directory. The config file will be watched for changes.
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
