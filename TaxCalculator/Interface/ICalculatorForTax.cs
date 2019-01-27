using System.Collections.Generic;
using TaxCalculator.Models;

namespace TaxCalculator.Interface
{
    public interface ICalculatorForTax
    {
        TaxAmount Save(TaxAmount item);

        IEnumerable<TaxAmount> GetAll();

        bool Update(TaxAmount taxAmount);

        bool Delete(int id);

        TaxAmount Get(int id);
    }
}
