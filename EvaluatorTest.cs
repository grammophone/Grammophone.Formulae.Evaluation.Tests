using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grammophone.Formulae.Evaluation.Tests
{
	[TestClass]
	public class EvaluatorTest
	{
		[TestMethod]
		public async Task CppScenario1()
		{
			var formulaDefinitions = CreateCppScenario1FormulaDefinitions();

			var employeeContext = new EmployeeContext
			{
				PI = 960.00M,
				PM = 12,
				P = 52
			};

			var formulaEvaluatorBuilder = new FormulaEvaluatorBuilder();

			var formulaEvaluator = formulaEvaluatorBuilder.CreateEvaluator<EmployeeContext>(formulaDefinitions);

			decimal A = await formulaEvaluator.EvaluateAsync<decimal>(employeeContext, "A");

			Assert.AreEqual(A, 49455.64M);
		}

		//[TestMethod]
		//public async Task Forbidden()
		//{
		//	var formulaDefinitions = CreateForbiddenDefinitions();

		//	var employeeContext = new EmployeeContext
		//	{
		//		PI = 960.00M,
		//		PM = 12,
		//		P = 52,
		//		B = 0.0M
		//	};

		//	var formulaEvaluator = new FormulaEvaluator<EmployeeContext>(formulaDefinitions);

		//	bool fileExists = await formulaEvaluator.EvaluateAsync<bool>(employeeContext, "fileExists");

		//	Assert.IsFalse(fileExists);

		//	System.Reflection.MethodBase currentMethod = await formulaEvaluator.EvaluateAsync<System.Reflection.MethodBase>(employeeContext, "currentMethod");

		//	Assert.IsNotNull(currentMethod);
		//}

		[TestMethod]
		public async Task AllowHostName()
		{
			var formulaDefinitions = CreateHostNameDefinitions();

			var employeeContext = new EmployeeContext
			{
				PI = 960.00M,
				PM = 12,
				P = 52,
				B = 0.0M
			};

			var references = new []
			{
				typeof(System.Net.Dns).Assembly
			};

			var formulaEvaluatorBuilder = new FormulaEvaluatorBuilder(references);

			var formulaEvaluator = formulaEvaluatorBuilder.CreateEvaluator<EmployeeContext>(formulaDefinitions);

			string? hostname = await formulaEvaluator.EvaluateAsync<string>(employeeContext, "hostName");

			Assert.AreEqual(hostname, System.Net.Dns.GetHostName());
		}

		[TestMethod]
		[ExpectedException(typeof(FormulaCompilationErrorException))]
		public async Task DenyHostName()
		{
			var formulaDefinitions = CreateHostNameDefinitions();

			var employeeContext = new EmployeeContext
			{
				PI = 960.00M,
				PM = 12,
				P = 52,
				B = 0.0M
			};

			var formulaEvaluatorBuilder = new FormulaEvaluatorBuilder();

			var formulaEvaluator = formulaEvaluatorBuilder.CreateEvaluator<EmployeeContext>(formulaDefinitions);

			_ = await formulaEvaluator.EvaluateAsync<string>(employeeContext, "hostName");
		}

		#region Private methods

		private static List<FormulaDefinition> CreateCppScenario1FormulaDefinitions()
		{
			var definitions = new List<FormulaDefinition>
			{
				new FormulaDefinition
				{
					Identifier = "A",
					DataType = typeof(decimal),
					Expression = "Round(P * (PI - F5A), 2, MidpointRounding.AwayFromZero)"
				},
				new FormulaDefinition
				{
					Identifier = "F5A",
					DataType = typeof(decimal),
					Expression = "Round(F5 * ((PI - B) / PI), 2, MidpointRounding.AwayFromZero)"
				},
				new FormulaDefinition
				{
					Identifier = "F5",
					DataType = typeof(decimal),
					Expression = "Round(C * (0.0100M / 0.0595M), 2, MidpointRounding.AwayFromZero)"
				},
				new FormulaDefinition
				{
					Identifier = "C",
					DataType = typeof(decimal),
					Expression = "Round(Min(3754.45M * (PM / 12), 0.0595M * (PI - (3500.00M / (decimal)P))), 2, MidpointRounding.AwayFromZero)"
				}
			};

			return definitions;
		}

		private static List<FormulaDefinition> CreateForbiddenDefinitions()
		{
			var definitions = new List<FormulaDefinition>
			{
				new FormulaDefinition
				{
					Identifier = "fileExists",
					DataType = typeof(bool),
					Expression = "System.IO.File.Exists(\"lele.txt\")"
				},
				new FormulaDefinition
				{
					Identifier = "currentMethod",
					DataType = typeof(System.Reflection.MethodBase),
					Expression = "System.Reflection.MethodInfo.GetCurrentMethod()"
				}
			};

			return definitions;
		}

		private static List<FormulaDefinition> CreateHostNameDefinitions()
		{
			var definitions = new List<FormulaDefinition>
			{
				new FormulaDefinition
				{
					Identifier = "hostName",
					DataType = typeof(string),
					Expression = "System.Net.Dns.GetHostName()"
				}
			};

			return definitions;
		}

		#endregion
	}
}