﻿using System;
using System.Diagnostics.Contracts;
using ReClassNET.Extensions;
using ReClassNET.Memory;

namespace ReClassNET.AddressParser
{
	public class Interpreter : IExecuter
	{
		public IntPtr Execute(IExpression expression, RemoteProcess process)
		{
			Contract.Requires(expression != null);
			Contract.Requires(process != null);

			switch (expression)
			{
				case ConstantExpression constantExpression:
#if RECLASSNET64
					return (IntPtr)constantExpression.Value;
#else
					return (IntPtr)unchecked((int)constantExpression.Value);
#endif
				case ModuleExpression moduleExpression:
				{
					var module = process.GetModuleByName(moduleExpression.Name);
					if (module != null)
					{
						return module.Start;
					}

					return IntPtr.Zero;
				}
				case AddExpression addExpression:
					return Execute(addExpression.Lhs, process).Add(Execute(addExpression.Rhs, process));
				case SubtractExpression subtractExpression:
					return Execute(subtractExpression.Lhs, process).Sub(Execute(subtractExpression.Rhs, process));
				case MultiplyExpression multiplyExpression:
					return Execute(multiplyExpression.Lhs, process).Mul(Execute(multiplyExpression.Rhs, process));
				case DivideExpression divideExpression:
					return Execute(divideExpression.Lhs, process).Div(Execute(divideExpression.Rhs, process));
				case ReadMemoryExpression readMemoryExpression:
					var readFromAddress = Execute(readMemoryExpression.Expression, process);
					if (readMemoryExpression.ByteCount == 4)
					{
						return (IntPtr)process.ReadRemoteInt32(readFromAddress);
					}
					else
					{
#if RECLASSNET64
						return (IntPtr)process.ReadRemoteInt64(readFromAddress);
#else
						return (IntPtr)unchecked((int)process.ReadRemoteUInt64(readFromAddress));
#endif
					}
				default:
					throw new ArgumentException($"Unsupported operation '{expression.GetType().FullName}'.");
			}
		}
	}
}
