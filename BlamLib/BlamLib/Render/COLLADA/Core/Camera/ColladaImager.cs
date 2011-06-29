/*
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
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using BlamLib.Render.COLLADA.Validation;

namespace BlamLib.Render.COLLADA.Core
{
	[SerializableAttribute()]
	[XmlTypeAttribute(AnonymousType = true)]
	public partial class ColladaImager : ColladaElement
	{
		#region Fields
		ColladaObjectElementList<ColladaTechnique> _technique;
		ColladaObjectElementList<ColladaExtra> _extra;
		#endregion

		#region Children
		[XmlElement("technique")]
		public List<ColladaTechnique> Technique
		{ get { return _technique.Value; } set { _technique.Value = value; } }

		[XmlElement("extra")]
		public List<ColladaExtra> Extra
		{ get { return _extra.Value; } set { _extra.Value = value; } }
		#endregion

		public ColladaImager() : base(Enums.ColladaElementType.Core_Imager)
		{
			Fields.Add(_technique = new ColladaObjectElementList<ColladaTechnique>());
			Fields.Add(_extra = new ColladaObjectElementList<ColladaExtra>());

			ValidationTests.Add(new ColladaIsNull(Enums.ColladaElementType.All, _technique));
			ValidationTests.Add(new ColladaListMinCount<ColladaTechnique>(Enums.ColladaElementType.All, _technique, 1));
		}

		//public override void ValidateElement(Enums.ColladaElementType parent_type)
		//{
		//    #region Validate Self
		//    if ((Technique == null) || (Technique.Count == 0))
		//        throw new ColladaValidationException(
		//            String.Format(ColladaValidationException.MinimumElementCount, 1, "technique"));
		//    #endregion

		//    #region Validate Children
		//    try
		//    {
		//        if (Technique != null)
		//            foreach (ColladaElement element in Technique) element.ValidateElement(m_element_type);
		//        if (Extra != null)
		//            foreach (ColladaElement element in Extra) element.ValidateElement(m_element_type);
		//    }
		//    catch (Exception e)
		//    {
		//        throw new ColladaValidationException(e);
		//    }
		//    #endregion
		//}
	}
}