using System;
using System.Collections;
using System.Collections.Generic;

namespace Parquet.File
{
    /// <summary>
    /// Packs/unpacks definition levels
    /// </summary>
    static class DefinitionPack
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="values">Values to compress. This operation modifies the list</param>
        /// <param name="maxDefinitionLevel"></param>
        /// <param name="hasValueFlags">Indicates where values are present and where not, optional.</param>
        /// <returns>Definitions for the input values</returns>
        public static List<int> RemoveNulls(IList values, int maxDefinitionLevel, List<bool> hasValueFlags = null)
        {
            var definitions = new List<int>(values.Count);

            for (int i = values.Count - 1; i >= 0; i--)
            {
                object value = values[i];
                if (value == null)
                {
                    int level = (hasValueFlags != null && !hasValueFlags[i])
                       ? Math.Max(1, maxDefinitionLevel - 1)
                       : 0;
                    definitions.Add(level);
                    values.RemoveAt(i);
                }
                else
                {
                    definitions.Add(maxDefinitionLevel);
                }
            }

            definitions.Reverse();
            return definitions;
        }

        public static IList InsertDefinitions(IList values, int maxDefinitionLevel, List<int> definitions, out List<bool> hasValueFlags)
        {
            hasValueFlags = new List<bool>();
            if (definitions == null || !values.IsNullable()) return values;


            for (int i = 0; i < definitions.Count; i++)
            {
                int def = definitions[i];
                bool hasValue = true;

                if (def == 0)
                {
                    values.Insert(i, null);
                }
                else if (def != maxDefinitionLevel)
                {
                    values.Insert(i, null); //stil need to insert something to keep consistent length
                    hasValue = false;
                }

                hasValueFlags.Add(hasValue);
            }

            return values;
        }
    }
}
