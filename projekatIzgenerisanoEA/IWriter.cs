///////////////////////////////////////////////////////////
//  IWriter.cs
//  Implementation of the Interface IWriter
//  Generated by Enterprise Architect
//  Created on:      19-mar-2018 23.02.24
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace projekatRES3 {
	public interface IWriter  {
        bool WriteToLoadBalancer(Code code, int value);
        bool TurnOn(bool turn);
        bool TurnOff(bool turn);
	}//end IWriter

}//end namespace projekatRES3