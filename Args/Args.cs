namespace Args
{
    using ArgumentMarshaler;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Args
    {
        private Dictionary<char, IArgumentMarshaler> marshalers;
        private ISet<char> argsFound; //List?
        private int currentArgument;

        public Args(string schema, string[] args)
        {
            marshalers = new Dictionary<char, IArgumentMarshaler>();
            argsFound = new HashSet<char>();

            parseSchema(schema);
            parseArgumentStrings(args.ToList());
        }

        private void parseSchema(string schema)
        {
            string[] schemaElements = schema.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string element in schemaElements)
            {
                parseSchemaElement(element.Trim());
            }
        }

        private void parseSchemaElement(string element)
        {
            char elementId = element[0];
            string elementTail = element.Substring(1);
            validateSchemaElementId(elementId);

            if(elementTail.Length == 0)
            {
                marshalers.Add(elementId, new BooleanArgumentMarshaler());
            }
            else if(elementTail.Equals("*"))
            {
                marshalers.Add(elementId, new StringArgumentMarshaler());
            }
            else if(elementTail.Equals("#"))
            {
                marshalers.Add(elementId, new IntegerArgumentMarshaler());
            }
            else if(elementTail.Equals("##"))
            {
                marshalers.Add(elementId, new DoubleArgumentMarshaler());
            }
            else if(elementTail.Equals("[*]"))
            {
                marshalers.Add(elementId, new StringArrayArgumentMarshaler());
            }
            else
            {
                throw new ArgsException(INVALID_ARGUMENT_FORMAT, elementId, elementTail);
            }
        }

        private void validateSchemaElementId(char elementId)
        {
            if(char.IsLetter(elementId) == false)
            {
                throw new ArgsException(INVALID_ARGUMENT_NAME, elementId, null);
            }
        }

        private void parseArgumentStrings(List<string> argsList)
        {
            for(currentArgument = 0; currentArgument < argsList.Count; index++)
            {
                var argString = argsList[currentArgument];
                if(argString.StartsWith("-"))
                {
                    parseArgumentCharacters(argString.Substring(1));
                }
                else
                {
                    currentArgument--;
                }
            }
        }

        private void parseArgumentCharacters(string argChars)
        {
            for(int i = 0; i < argChars.Length; i++)
            {
                parseArgumentCharacter(argChars[i]);
            }
        }

        private void parseArgumentCharacter(char argChar)
        {
            ArgumentMarshaler m;
            if(marshalers.TryGetValue(argChar, out m) == false)
            {
                throw new ArgsException(UNEXPECTED_ARGUMENT, argChar, null);
            }
            else
            {
                argsFound.Add(argChar);
                try
                {
                    m.set(currentArgument);
                }
                catch (ArgsException e)
                {
                    e.setErrorArgumentId(argChar);
                    throw e;
                }
            }
        }

        public bool has(char arg)
        {
            return argsFound.Contains(arg);
        }

        public int nextArgument()
        {
            currentArgument++;
            return currentArgument;
        }

        public bool getBoolean(char arg)
        {
            return BooleanArgumentMarshaler.getValue(marshalers[arg]);
        }

        public string getString(char arg)
        {
            return StringArgumentMarshaler.getValue(marshalers[arg]);
        }

        public int getInt(char arg)
        {
            return IntArgumentMarshaler.getValue(marshalers[arg]);
        }

        public double getDouble(char arg)
        {
            return DoubleArgumentMarshaler.getValue(marshalers[arg]);
        }

        public string[] getStringArray(char arg)
        {
            return StringArrayArgumentMarshaler.getValue(marshalers[arg]);
        }
    }
}
