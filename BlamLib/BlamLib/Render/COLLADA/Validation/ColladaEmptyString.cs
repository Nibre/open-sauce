﻿/*
    BlamLib: .NET SDK for the Blam Engine

    Copyright (C) 2005-2010  Kornner Studios (http://kornner.com)

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;

namespace BlamLib.Render.COLLADA.Validation
{
	/// <summary>
	/// Validation test that check whether a string is empty
	/// </summary>
	public class ColladaEmptyString : ColladaValidationTest
	{
		private ColladaObject testField;

		/// <summary>
		/// Validation test constructor
		/// </summary>
		/// <param name="valid_parent">The parent collada element type to run this test for. Set to "All" to run regardless of parent type</param>
		/// <param name="field">The field to run the test on</param>
		public ColladaEmptyString(Enums.ColladaElementType valid_parent, ColladaObject field)
			: base(valid_parent)
		{
			testField = field;
		}

		/// <summary>
		/// Performs the validation test
		/// </summary>
		/// <returns></returns>
		protected override ColladaValidationException ValidateImpl()
		{
			// get the string value
			string test_string = (testField.GetValue() as string);

			// if the value is null, return null
			// there is a specific null value validation test for checking this
			if (test_string == null)
				return null;

			ColladaValidationException exception = null;
			if (test_string.Length == 0)
			{
				exception = new ColladaValidationException(
					String.Format(exceptionFormat, "EmptyString", testField.GetTypeName(), "a necessary strings length is zero"));
			}
			return exception;
		}
	}
}