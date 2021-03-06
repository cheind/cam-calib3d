﻿///
/// <project>Calib3D http://code.google.com/p/cam-calib3d/ </project>
/// <author>Christoph Heindl</author>
/// <copyright>Copyright (c) 2011, Christoph Heindl</copyright>
/// <license>New BSD License</license>
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Test.CommandLineParsing;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading;

namespace CommandLineExamples {

  /// <summary>
  /// Take snapshots from capture device.
  /// </summary>
  /// <example>
  /// CommandLineExamples.exe run CameraSnapshot /OutputDirectory=. /HardwareId=0 /FrameSize=1024,768
  /// </example>
  public class CameraSnapshot : IExample {

    /// <summary>
    /// Supported commandline arguments.
    /// </summary>
    class CommandLineArguments {

      [Description("Print this help")]
      public bool? Help { get; set; }

      [Description("Path to store snapshots to")]
      public string OutputDirectory { get; set; }

      [Description("Desired image size")]
      public System.Drawing.Size? FrameSize { get; set; }

      [Description("Device ID of camera")]
      public int? HardwareId { get; set; }
    }

    public string Description {
      get { return "Take snapshots from capture device."; }
    }

    public void Run(string[] args) {
      CommandLineArguments a = new CommandLineArguments();
      CommandLineParser.ParseArguments(a, args);

      if (a.Help.GetValueOrDefault(false)) {
        CommandLineParser.PrintUsage(a);
        return;
      }

      System.Console.WriteLine("In the commandline window");
      System.Console.WriteLine("  Press ESCAPE to quit example");
      System.Console.WriteLine("  Press any other key to take a snapshot");

      string output_path = Default.Get(a.OutputDirectory, ".");
      int device_id = Default.Get(a.HardwareId, 0);

      if (!System.IO.Directory.Exists(output_path)) {
        System.IO.Directory.CreateDirectory(output_path);
      }

      // Start capturing from camera.
      Capture capture = new Capture(device_id);
      if (a.FrameSize.HasValue) {
        capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, a.FrameSize.Value.Width);
        capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, a.FrameSize.Value.Height);
      }

      bool stop = false;
      while (!stop) {
        Emgu.CV.Image<Bgr, byte> i = capture.QueryFrame();
        Calib3D.IO.Images.Show(i, 30, "LiveFeed");
        if (Console.KeyAvailable) {
          ConsoleKeyInfo ki = Console.ReadKey();
          if (ki.Key == ConsoleKey.Escape) {
            stop = true;
          } else {
            string filename = string.Format("snapshot-{0}.png", Guid.NewGuid());
            Console.WriteLine(String.Format("Saving {0}", filename));
            i.Save(System.IO.Path.Combine(output_path, filename));
          }
        }
      }
    }
  }
}
