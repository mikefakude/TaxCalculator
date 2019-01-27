using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using TaxCalculator.Interface;
using TaxCalculator.Models;

namespace TaxCalculator.Repositories
{
    public class CalculatorForTax : ICalculatorForTax
    {
        TaxCalculatorEntities taxCalculator = new TaxCalculatorEntities();

        public IEnumerable<TaxAmount> GetAll()
        {
            return taxCalculator.TaxAmounts;
        }

        public TaxAmount Get(int id)
        {
            return taxCalculator.TaxAmounts.Find(id);
        }

        public TaxAmount Save(TaxAmount item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            try
            {
                var calculateTax = new CalculateTax();
                calculateTax.CalculatingTaxForProgressive(item);
                calculateTax.CalculatingTaxForFlatValue(item);
                calculateTax.CalculatingTaxForFlatRate(item);

                taxCalculator.TaxAmounts.Add(item);
                taxCalculator.SaveChanges();
                return item;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public bool Update(TaxAmount taxAmount)
        {
            if (taxAmount == null)
            {
                throw new ArgumentNullException("taxAmount");
            }

            var getValues = taxCalculator.TaxAmounts.Single(a => a.Id == taxAmount.Id);
            getValues.PostalCode = taxAmount.PostalCode;
            getValues.TaxIncome = taxAmount.TaxIncome;
            taxCalculator.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var taxAmount = taxCalculator.TaxAmounts.Find(id);
            taxCalculator.TaxAmounts.Remove(taxAmount);
            taxCalculator.SaveChanges();

            return true;
        }
    }
}