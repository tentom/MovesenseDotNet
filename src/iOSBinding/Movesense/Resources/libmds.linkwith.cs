using System;
using ObjCRuntime;

[assembly: LinkWith ("libmds.a", LinkTarget.ArmV7 | LinkTarget.Arm64 | LinkTarget.Simulator, IsCxx =true, SmartLink =true, ForceLoad = false, LinkerFlags ="-lz")]
