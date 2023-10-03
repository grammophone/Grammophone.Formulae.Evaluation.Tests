using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grammophone.Formulae.Evaluation.Tests
{
	public class FormulaDefinition : IFormulaDefinition
	{
		private static int lastID = 0;

		public FormulaDefinition()
		{
			this.ID = Interlocked.Increment(ref lastID);

			this.DataType = typeof(int);
			this.Identifier = String.Empty;
			this.Expression = String.Empty;
		}

		public int ID { get; }

		public Type DataType { get; init; }

		public string Identifier { get; init; }

		public string Expression { get; init; }

		public string GetFormulaID() => this.ID.ToString();
	}
}
