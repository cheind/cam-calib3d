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
using System.ComponentModel.Composition;
using System.ComponentModel;

namespace Calib3D {

  /// <summary>
  /// Base class for all patterns.
  /// </summary>
  [InheritedExport]
  public class Pattern : INotifyPropertyChanged {

    /// <summary>
    /// Property change notifications
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Create empty pattern.
    /// </summary>
    public Pattern() { }

    /// <summary>
    /// Triggers the property changed event
    /// </summary>
    /// <param name="property_name"></param>
    protected void TriggerPropertyChanged(string property_name) {
      if (PropertyChanged != null) {
        PropertyChanged(this, new PropertyChangedEventArgs(property_name));
      }
    }

  }
}
