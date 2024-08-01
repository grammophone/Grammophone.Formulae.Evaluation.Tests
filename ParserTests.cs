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
			var formulaEvaluatorFactory = new FormulaEvaluatorFactory<EmployeeContext>();

			string expression = "2 + 2";

			var parser = formulaEvaluatorFactory.GetFormulaParser();

			var diagnostics = parser.Validate(expression);

			Assert.IsFalse(diagnostics.Any(), "There should be no errors.");
		}

		[TestMethod]
		public void DiagnoseMissingParenthesis()
		{
			var formulaEvaluatorFactory = new FormulaEvaluatorFactory<EmployeeContext>();

			string expression = "(2 + 2";

			var parser = formulaEvaluatorFactory.GetFormulaParser();

			var diagnostics = parser.Validate(expression);

			Assert.IsTrue(diagnostics.Any(), "There should be an error.");
		}

		[TestMethod]
		public void DiagnosValidVariables()
		{
			var formulaEvaluatorFactory = new FormulaEvaluatorFactory<EmployeeContext>();

			string expression = "42 + B";

			var parser = formulaEvaluatorFactory.GetFormulaParser();

			var diagnostics = parser.Validate(expression);

			Assert.IsFalse(diagnostics.Any(), "There should be no errors.");
		}

		[TestMethod]
		[ExpectedException(typeof(FormulaNameAccessException))]
		public void FailForbiddenName()
		{
			string[] excludedNames = new[] { "B" }; 

			var formulaEvaluatorFactory = new FormulaEvaluatorFactory<EmployeeContext>(excludedNames: excludedNames);

			string expression = "42 + B";

			var parser = formulaEvaluatorFactory.GetFormulaParser();

			var diagnostics = parser.Validate(expression);

			Assert.IsFalse(diagnostics.Any(), "There should be no errors.");
		}
	}
}
