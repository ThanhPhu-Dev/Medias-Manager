﻿using Manager_Medias.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Manager_Medias.ViewModels
{
    public class BaseViewModel : DependencyObject, INotifyPropertyChanged, INotifyDataErrorInfo
    {
        protected static UserStore _userStore;

        #region Navigate

        protected static NavigationStore _navigationStore;

        public BaseViewModel ContentViewModel => _navigationStore.ContentViewModel;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        #endregion Navigate

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion PropertyChanged

        #region InvokeViewChanged

        protected void _navigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        protected void _navigationStore_CurrentContentViewModelChanged()
        {
            OnPropertyChanged(nameof(ContentViewModel));
        }

        #endregion InvokeViewChanged

        #region InvokeUserChanged

        protected void _userStore_CurrentUserChanged()
        {
            OnPropertyChanged("Email");
        }

        #endregion InvokeUserChanged

        #region INotifyDataErrorInfo

        public Dictionary<string, List<string>> Errors { get; set; }
        public Dictionary<string, List<ValidationRule>> ValidationRules { get; set; }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get
            {
                if (this.Errors == null)
                {
                    return false;
                }
                else
                {
                    return this.Errors.Any();
                }
            }
        }

        // Validation method.
        // Is called from each property which needs to validate its value.
        // Because the parameter 'propertyName' is decorated with the 'CallerMemberName' attribute.
        // this parameter is automatically generated by the compiler.
        // The caller only needs to pass in the 'propertyValue', if the caller is the target property's set method.
        public bool ValidateProperty<TValue>(TValue propertyValue, [CallerMemberName] string propertyName = null)
        {
            // Clear previous errors of the current property to be validated
            this.Errors.Remove(propertyName);
            OnErrorsChanged(propertyName);

            if (this.ValidationRules.TryGetValue(propertyName, out List<ValidationRule> propertyValidationRules))
            {
                // Apply all the rules that are associated with the current property
                // and validate the property's value
                propertyValidationRules
                  .Select(validationRule => validationRule.Validate(propertyValue, CultureInfo.CurrentCulture))
                  .Where(result => !result.IsValid)
                  .ToList()
                  .ForEach(invalidResult => AddError(propertyName, invalidResult.ErrorContent as string));

                return !PropertyHasErrors(propertyName);
            }

            // No rules found for the current property
            return true;
        }

        // Adds the specified error to the errors collection if it is not
        // already present, inserting it in the first position if 'isWarning' is
        // false. Raises the ErrorsChanged event if the Errors collection changes.
        // A property can have multiple errors.
        public void AddError(string propertyName, string errorMessage, bool isWarning = false)
        {
            if (!this.Errors.TryGetValue(propertyName, out List<string> propertyErrors))
            {
                propertyErrors = new List<string>();
                this.Errors[propertyName] = propertyErrors;
            }

            if (!propertyErrors.Contains(errorMessage))
            {
                if (isWarning)
                {
                    // Move warnings to the end
                    propertyErrors.Add(errorMessage);
                }
                else
                {
                    propertyErrors.Insert(0, errorMessage);
                }
                OnErrorsChanged(propertyName);
            }
        }

        public bool PropertyHasErrors(string propertyName) => this.Errors.TryGetValue(propertyName, out List<string> propertyErrors) && propertyErrors.Any();

        // Returns all errors of a property. If the argument is 'null' instead of the property's name,
        // then the method will return all errors of all properties.
        public System.Collections.IEnumerable GetErrors(string propertyName)
          => string.IsNullOrWhiteSpace(propertyName)
            ? this.Errors.SelectMany(entry => entry.Value)
            : this.Errors.TryGetValue(propertyName, out List<string> errors)
              ? errors
              : new List<string>();

        protected virtual void OnErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion INotifyDataErrorInfo
    }
}