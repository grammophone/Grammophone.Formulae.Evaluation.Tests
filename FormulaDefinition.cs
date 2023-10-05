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

		public Type DataType { get; internal set; }

		public string Identifier { get; internal set; }

		public string Expression { get; internal set; }

		public string GetFormulaID() => this.ID.ToString();
	}
}
