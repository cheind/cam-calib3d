﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.IO;

namespace TryOutZone {
  class Program {
    static void Main(string[] args) {
      AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

      Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> i = new Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>("marker.png");

      


      Calib3D.Addins a = new Calib3D.Addins(Calib3D.Addins.DefaultCatalog);

      /*
      Calib3D.Addins a = new Calib3D.Addins(Calib3D.Addins.DefaultCatalog);
      Calib3D.Pattern p = a.FindByFullName(a.Patterns, "Calib3D.Marker.MarkerDetector");
      Calib3D.PatternDetector pd = a.FindDetectorsSupportingPattern(a.Detectors, p).FirstOrDefault();

      Calib3D.DetectionResult dr = pd.FindPattern(p, i);
      dr.PatternDetector.OverlayProvider.Overlay(i, dr);
       */

      Calib3D.Marker.MarkerPattern p = new Calib3D.Marker.MarkerPattern();
      Calib3D.Marker.MarkerDetector md = new Calib3D.Marker.MarkerDetector();

      p.MarkerImage = new Image<Emgu.CV.Structure.Bgr, byte>("marker_a.png");
      p.MarkerLength = 50;
      md.Pattern = p;

      Calib3D.DetectionResult dr = md.FindPattern(i);
      dr.PatternDetector.OverlayProvider.Overlay(i, dr);

      
      CvInvoke.cvNamedWindow("x");
      CvInvoke.cvShowImage("x", i.Ptr);
      //Wait for the key pressing event
      CvInvoke.cvWaitKey(0);
      //Destory the window
      CvInvoke.cvDestroyWindow("x"); 
    }

    static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
      Assembly asm = Assembly.LoadFrom(Path.Combine("Addins", args.Name + ".dll"));
      return asm;
    }
  }
}