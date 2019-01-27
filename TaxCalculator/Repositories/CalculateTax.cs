using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxCalculator.Models;

namespace TaxCalculator.Repositories
{
    public class CalculateTax
    {
        public TaxAmount CalculatingTaxForProgressive(TaxAmount taxAmount)
        {

            //Calculate Progressive tax of code 7441 and 100
            if ((taxAmount.PostalCode == ConstantValues.PostalCodeProgressive  || 
                taxAmount.PostalCode == ConstantValues.OtherProgressivePostalCode) && 
                taxAmount.TaxIncome <= 8350)
            {
                taxAmount.CalculatedValue = taxAmount.TaxIncome * ConstantValues.TenPercent;
            }
            else if((taxAmount.PostalCode == ConstantValues.PostalCodeProgressive || 
                    taxAmount.PostalCode == ConstantValues.OtherProgressivePostalCode) && 
                    (taxAmount.TaxIncome <= 33950 || taxAmount.TaxIncome >= 8351))
            {
                taxAmount.CalculatedValue = taxAmount.TaxIncome * ConstantValues.FifteenPercent;
            }
            else if ((taxAmount.PostalCode == ConstantValues.PostalCodeProgressive || 
                taxAmount.PostalCode == ConstantValues.OtherProgressivePostalCode) &&
                (taxAmount.TaxIncome <= 82250 || taxAmount.TaxIncome >= 33951))
            {
                taxAmount.CalculatedValue = taxAmount.TaxIncome * ConstantValues.TwentyFivePercent;
            }
            else if ((taxAmount.PostalCode == ConstantValues.PostalCodeProgressive || 
                     taxAmount.PostalCode == ConstantValues.OtherProgressivePostalCode) &&
                     (taxAmount.TaxIncome  <= 171550 || taxAmount.TaxIncome >= 82251))
            {
                taxAmount.CalculatedValue = taxAmount.TaxIncome * ConstantValues.TwentyEightPercent;
            }
            else if ((taxAmount.PostalCode == ConstantValues.PostalCodeProgressive || 
                     taxAmount.PostalCode == ConstantValues.OtherProgressivePostalCode) && 
                     (taxAmount.TaxIncome <= 372950 || taxAmount.TaxIncome >= 171551))
            {
                taxAmount.CalculatedValue = taxAmount.TaxIncome * ConstantValues.ThirtyThreePercent;
            }
            else if ((taxAmount.PostalCode == ConstantValues.PostalCodeProgressive ||
                      taxAmount.PostalCode == ConstantValues.OtherProgressivePostalCode) &&
                      (taxAmount.TaxIncome >= 372951))
            {
                taxAmount.CalculatedValue = taxAmount.TaxIncome * ConstantValues.ThirtyFivePercent;
            }
            else
            {
                taxAmount.CalculatedValue = 0;
            }

            return taxAmount;

        }

        public TaxAmount CalculatingTaxForFlatValue(TaxAmount taxAmount)
        {
            //Calculate The flat value:
            if (taxAmount.PostalCode == ConstantValues.PostalCodeFlatValue && taxAmount.TaxIncome <= 171550)
            {
                taxAmount.CalculatedValue = taxAmount.TaxIncome * ConstantValues.FivePercent;
            }
            else
            {
                taxAmount.CalculatedValue = 0;
            }

            return taxAmount;
        }

        public TaxAmount CalculatingTaxForFlatRate(TaxAmount taxAmount)
        {
            //Calculate The flat rate:
            if (taxAmount.PostalCode == ConstantValues.PostalCodeFlatRate && taxAmount.TaxIncome >= 171550)
            {
                taxAmount.CalculatedValue = taxAmount.TaxIncome * ConstantValues.SeventeenPercent;
            }
            else
            {
                taxAmount.CalculatedValue = 0;
            }

            return taxAmount;
        }
    }
}