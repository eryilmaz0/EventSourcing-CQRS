﻿namespace Command.Application.Exception;

public class ConsistencyException : System.Exception
{
    public ConsistencyException():base("Expected Version Number did not match.") {}
        
    
    
}