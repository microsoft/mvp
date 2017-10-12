using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Microsoft.Mvp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewModelBase : BaseViewModel
    {
        /// <summary>
        /// Event for when IsValid changes
        /// </summary>
        public event EventHandler IsValidChanged;

        readonly List<string> errors = new List<string>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public ViewModelBase()
        {
            //Make sure validation is performed on startup
            Validate();
        }

        /// <summary>
        /// Returns true if the current state of the ViewModel is valid
        /// </summary>
        public bool IsValid => errors.Count == 0; 

        /// <summary>
        /// A list of errors if IsValid is false
        /// </summary>
        protected List<string> Errors => errors;

        /// <summary>
        /// Protected method for validating the ViewModel
        /// - Fires PropertyChanged for IsValid and Errors
        /// </summary>
        protected virtual void Validate()
        {
            OnPropertyChanged("IsValid");
            IsValidChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Other viewmodels should call this when overriding Validate, to validate each property
        /// </summary>
        /// <param name="validate">Func to determine if a value is valid</param>
        /// <param name="myError">The error message to use if not valid</param>
        protected virtual void ValidateProperty(Func<bool> validate, string myError)
        {
            if (validate())
            {
                if (!Errors.Contains(myError))
                    Errors.Add(myError);
            }
            else
            {
                Errors.Remove(myError);
            }
        }
    }
}
