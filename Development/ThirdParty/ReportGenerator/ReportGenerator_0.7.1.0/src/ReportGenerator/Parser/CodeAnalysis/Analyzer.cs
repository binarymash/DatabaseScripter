using System;
using System.Collections.Generic;
using ICSharpCode.NRefactory.Ast;

namespace Palmmedia.ReportGenerator.Parser.CodeAnalysis
{
    /// <summary>
    /// Helper class to determine the begin and end line number of methods within a source code file.
    /// This class is used to compensate a deficiency of PartCover 2.3.0.35109:
    /// PartCover does not record coverage information for unexecuted methods any more.
    /// To provide correct reports the line numbers are determined from the source code files instead of
    /// PartCover's coverage information.
    /// </summary>
    internal static class Analyzer
    {
        /// <summary>
        /// The name of the last source code file that has successfully been parsed.
        /// </summary>
        private static string lastFilename;

        /// <summary>
        /// The <see cref="ICSharpCode.NRefactory.Ast.INode"/> of the last source code file that has successfully been parsed.
        /// </summary>
        private static INode lastNode;        

        /// <summary>
        /// Searches the given source code file for a method matching the given <see cref="MethodInfo"/>.
        /// If the method can be found, a <see cref="MethodResult"/> containing the start and end line numbers is returned.
        /// Otherwise <c>null</c> is returned.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="methodInfo">The method info.</param>
        /// <returns>A <see cref="MethodResult"/> or <c>null</c> if method can not be found.</returns>
        public static MethodResult FindMethod(string filename, MethodInfo methodInfo)
        {
            if (filename == null)
            {
                throw new ArgumentNullException("filename");
            }

            if (methodInfo == null)
            {
                throw new ArgumentNullException("methodInfo");
            }

            // Is the node available in cache?
            if (filename.Equals(lastFilename))
            {
                return FindMethod(new INode[] { lastNode }, methodInfo);
            }
            else
            {
                try
                {
                    using (var parser = ICSharpCode.NRefactory.ParserFactory.CreateParser(filename))
                    {
                        if (parser == null)
                        {
                            return null;
                        }

                        parser.Parse();

                        if (parser.Errors.Count != 0 || parser.CompilationUnit == null || parser.CompilationUnit.CurrentBock == null)
                        {
                            return null;
                        }
                        else
                        {
                            // Cache the node
                            lastFilename = filename;
                            lastNode = parser.CompilationUnit.CurrentBock;

                            return FindMethod(new INode[] { parser.CompilationUnit.CurrentBock }, methodInfo);
                        }
                    }            
                }
                catch (System.IO.IOException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Searches the given <see cref="ICSharpCode.NRefactory.Ast.INode">INodes</see> recursivly for the given method.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="methodInfo">The method info.</param>
        /// <returns>A <see cref="MethodResult"/> or <c>null</c> if method can not be found.</returns>
        private static MethodResult FindMethod(IEnumerable<INode> nodes, MethodInfo methodInfo)
        {
            MethodResult result;

            foreach (var node in nodes)
            {
                if (TryGetMethodResult(node, methodInfo, out result))
                {
                    return result;
                }
            }

            foreach (var node in nodes)
            {
                result = FindMethod(node.Children, methodInfo);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Retrives the <see cref="MethodResult"/> from the given <see cref="INode"/> if it matches the <see cref="MethodInfo"/>.
        /// The return value indicates whether the operation was successful.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="methodInfo">The method info.</param>
        /// <param name="methodResult">The method result.</param>
        /// <returns><c>true</c> if <see cref="INode"/> matches the <see cref="MethodInfo"/>, otherwise <c>false</c>.</returns>
        private static bool TryGetMethodResult(INode node, MethodInfo methodInfo, out MethodResult methodResult)
        {
            methodResult = null;

            if (methodInfo.IsConstructor)
            {
                ConstructorDeclaration constructorDeclaration = node as ConstructorDeclaration;

                if (constructorDeclaration != null)
                {
                    if (!methodInfo.DoesMethodnameMatch(constructorDeclaration.Name))
                    {
                        return false;
                    }

                    if (!methodInfo.AreParametersMatching(constructorDeclaration.Parameters))
                    {
                        return false;
                    }

                    if (constructorDeclaration.Body != null)
                    {
                        methodResult = new MethodResult(constructorDeclaration.Body.StartLocation.Line, constructorDeclaration.Body.EndLocation.Line);
                    }

                    return true;
                }

                return false;
            }

            MethodDeclaration methodDeclaration = node as MethodDeclaration;
            if (methodDeclaration != null)
            {
                if (!methodInfo.DoesMethodnameMatch(methodDeclaration.Name))
                {
                    return false;
                }

                if (!methodInfo.DoesReturnTypeMatch(methodDeclaration.TypeReference))
                {
                    return false;
                }

                if (!methodInfo.AreParametersMatching(methodDeclaration.Parameters))
                {
                    return false;
                }

                if (methodDeclaration.Body != null)
                {
                    methodResult = new MethodResult(methodDeclaration.Body.StartLocation.Line, methodDeclaration.Body.EndLocation.Line);
                }

                return true;
            }

            return false;
        }
    }
}
