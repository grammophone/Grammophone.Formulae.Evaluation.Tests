using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grammophone.Formulae.Evaluation.Tests
{
	[TestClass]
	public class ParserTests
	{
		[TestMethod]
		public void DiagnoseNoError()
		{
			var formulaFactory = new FormulaFactory<EmployeeContext>();

			string expression = "2 + 2";

			var parser = formulaFactory.GetParser();

			var diagnostics = parser.Validate(expression);

			Assert.IsFalse(diagnostics.Any(), "There should be no errors.");
		}

		[TestMethod]
		public void DiagnoseMissingParenthesis()
		{
			var formulaFactory = new FormulaFactory<EmployeeContext>();

			string expression = "(2 + 2";

			var parser = formulaFactory.GetParser();

			var diagnostics = parser.Validate(expression);

			Assert.IsTrue(diagnostics.Any(), "There should be an error.");
		}

		[TestMethod]
		public void DiagnosValidVariables()
		{
			var formulaFactory = new FormulaFactory<EmployeeContext>();

			string expression = "42 + B";

			var parser = formulaFactory.GetParser();

			var diagnostics = parser.Validate(expression);

			Assert.IsFalse(diagnostics.Any(), "There should be no errors.");
		}

		[TestMethod]
		[ExpectedException(typeof(FormulaNameAccessException))]
		public void FailForbiddenName()
		{
			string[] excludedNames = new[] { "B" }; 

			var formulaFactory = new FormulaFactory<EmployeeContext>(excludedNames: excludedNames);

			string expression = "42 + B";

			var parser = formulaFactory.GetParser();

			var diagnostics = parser.Validate(expression);

			Assert.IsFalse(diagnostics.Any(), "There should be no errors.");
		}
	}
}
