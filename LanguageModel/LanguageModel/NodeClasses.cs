﻿using ImmutableObjectGraph;
using ImmutableObjectGraph.CodeGeneration;
using LanguageService.LanguageModel.TreeVisitors;
using System;
using System.Collections.Immutable;
using System.IO;

namespace LanguageService
{

    [GenerateImmutable(GenerateBuilder = true)]
    public abstract partial class SyntaxNode
    {
        [Required]
        readonly int startPosition;
        [Required]
        readonly int length;

        public abstract void Accept(INodeVisitor visitor);
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public abstract partial class StatementNode : SyntaxNode { }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class MisplacedTokenNode : StatementNode
    {
        [Required]
        readonly Token token;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class SemiColonStatementNode : StatementNode
    {
        [Required]
        readonly Token semiColon;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class ChunkNode : SyntaxNode
    {
        [Required]
        readonly BlockNode programBlock;
        [Required]
        readonly Token endOfFile;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class BlockNode : SyntaxNode
    {
        [Required]
        [NotRecursive]
        readonly ImmutableList<StatementNode> statements;
        readonly ReturnStatementNode returnStatement;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    #region If Statement Nodes
    [GenerateImmutable(GenerateBuilder = true)]
    public partial class IfStatementNode : StatementNode
    {
        [Required]
        readonly Token ifKeyword;
        [Required]
        readonly ExpressionNode exp;
        [Required]
        readonly Token thenKeyword;
        [Required]
        readonly BlockNode ifBlock;
        [Required, NotRecursive]
        readonly ImmutableList<ElseIfBlockNode> elseIfList;
        readonly ElseBlockNode elseBlock;
        [Required]
        readonly Token endKeyword;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class ElseBlockNode : SyntaxNode
    {
        [Required]
        readonly Token elseKeyword;
        [Required]
        readonly BlockNode block;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class ElseIfBlockNode : SyntaxNode
    { 
        [Required]
        readonly Token elseIfKeyword;
        [Required]
        readonly ExpressionNode exp;
        [Required]
        readonly Token thenKeyword;
        [Required]
        readonly BlockNode block;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
    #endregion

    #region Expression nodes
    [GenerateImmutable(GenerateBuilder = true)]
    public abstract partial class ExpressionNode : SyntaxNode { }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class SimpleExpression : ExpressionNode
    {
        [Required]
        readonly Token expressionValue;
        public static bool IsValidExpressionNode(TokenType type)
        {
            switch (type)
            {
                case TokenType.Number:
                case TokenType.TrueKeyValue:
                case TokenType.FalseKeyValue:
                case TokenType.NilKeyValue:
                case TokenType.VarArgOperator:
                case TokenType.String:
                    return true;
                default:
                    return false;
            }
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class BinaryOperatorExpression : ExpressionNode
    {
        [Required]
        readonly ExpressionNode exp1;
        [Required]
        readonly Token binaryOperator;
        [Required]
        readonly ExpressionNode exp2;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class UnaryOperatorExpression : ExpressionNode
    {
        [Required]
        readonly Token unaryOperator;
        [Required]
        readonly ExpressionNode exp;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
    #endregion

    #region Other Expression Nodes (out of scope for Code review)
    [GenerateImmutable(GenerateBuilder = true)]
    public partial class TableConstructorExp : ExpressionNode
    {
        [Required]
        readonly Token openCurly;
        FieldList fieldList;
        [Required]
        readonly Token closeCurly;
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class FunctionDef : ExpressionNode
    {
        [Required]
        readonly Token functionKeyword;
        [Required]
        readonly FuncBodyNode functionBody;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public abstract partial class PrefixExp : ExpressionNode { }

    [GenerateImmutable(GenerateBuilder = true)]
    public abstract partial class Var : PrefixExp { }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class NameVar : Var
    {
        [Required]
        readonly Token name;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class SquareBracketVar : Var
    {
        [Required]
        readonly PrefixExp prefixExp;
        [Required]
        readonly Token openBracket;
        [Required]
        readonly ExpressionNode exp;
        [Required]
        readonly Token closeBracket;
        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class DotVar : Var
    {
        [Required]
        readonly PrefixExp prefixExp;
        [Required]
        readonly Token dotOperator;
        [Required]
        readonly Token nameIdentifier;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class FunctionCallExp : PrefixExp 
    {
        [Required]
        readonly PrefixExp prefixExp;
        readonly Token colon;
        readonly Token name;
        [Required]
        readonly Args args;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
    [GenerateImmutable(GenerateBuilder = true)]
    public partial class FunctionCallStatement : StatementNode 
    {
        [Required]
        readonly PrefixExp prefixExp;
        readonly Token colon;
        readonly Token name;
        [Required]
        readonly Args args;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
    
    [GenerateImmutable(GenerateBuilder = true)]
    public partial class ParenPrefixExp : PrefixExp
    {
        [Required]
        readonly Token openParen;
        [Required]
        readonly ExpressionNode exp;
        [Required]
        readonly Token closeParen;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        //internal override void ToString(TextWriter writer)
        //{
        //    var indentingWriter = IndentingTextWriter.Get(writer);
        //    indentingWriter.WriteLine("ParenPrefixExp");
        //    using (indentingWriter.Indent())
        //    {
        //        if (exp != null)
        //        {
        //            exp.ToString(indentingWriter);
        //        } else
        //        {
        //            indentingWriter.WriteLine("null");
        //        }
        //    }
        //}
    }
    #endregion

    #region Args Nodes
    [GenerateImmutable(GenerateBuilder = true)]
    public abstract partial class Args : SyntaxNode { }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class TableContructorArg : Args
    {
        [Required]
        readonly Token openCurly;
        readonly FieldList fieldList;
        [Required]
        readonly Token closeCurly;

        public override void Accept(INodeVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class ParenArg : Args
    {
        [Required]
        readonly Token openParen;
        [Required]
        readonly ExpList expList;
        [Required]
        readonly Token closeParen;

        public override void Accept(INodeVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class StringArg : Args
    {
        [Required]
        readonly Token stringLiteral;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
    #endregion

    #region List nodes
    [GenerateImmutable(GenerateBuilder = true)]
    public partial class NameList : SyntaxNode
    {
        [Required]
        readonly ImmutableList<NameCommaPair> names;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class FieldList : SyntaxNode
    {
        [Required]
        readonly ImmutableList<FieldAndSeperatorPair> fields;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class ExpList : SyntaxNode
    {
        [Required]
        readonly ImmutableList<ExpressionCommaPair> expressions;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public abstract partial class ParList : SyntaxNode { }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class VarArgPar : ParList
    {
        [Required]
        readonly Token varargOperator;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class NameListPar : ParList
    {
        [Required]
        readonly NameList namesList;
        [Required]
        readonly CommaVarArgPair varArgPar;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class CommaVarArgPair
    {
        [Required]
        readonly Token comma;
        [Required]
        readonly Token varargOperator;
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class NameCommaPair
    {
        [Required]
        readonly Token comma;
        [Required]
        readonly Token name;
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class ExpressionCommaPair
    {
        [Required]
        readonly Token comma;
        [Required]
        readonly ExpressionNode expression;
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class FieldAndSeperatorPair
    {
        readonly FieldNode field;
        readonly Token fieldSeparator;
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public abstract partial class FieldNode : ExpressionNode { } //TODO: is this inheritance okay?

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class BracketField : FieldNode
    {
        [Required]
        readonly Token openBracket;
        [Required]
        readonly ExpressionNode identifierExp;
        [Required]
        readonly Token closeBracket;
        [Required]
        readonly Token assignmentOperator;
        [Required]
        readonly ExpressionNode assignedExp;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class SimpleField : FieldNode
    {
        [Required]
        readonly Token name;
        [Required]
        readonly Token assignmentOperator;
        [Required]
        readonly ExpressionNode exp;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class ExpField : FieldNode
    {
        [Required]
        readonly ExpressionNode exp;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
    #endregion

    #region Other Nodes
    [GenerateImmutable(GenerateBuilder = true)]
    public partial class ReturnStatementNode : SyntaxNode
    {
        [Required]
        readonly Token returnKeyword;
        readonly ExpList returnExpressions;
        //Token semiColonRetStat; Question: is this really necessary even though defined in the language?

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        //internal override void ToString(TextWriter writer)
        //{
        //    var indentingWriter = IndentingTextWriter.Get(writer);
        //    indentingWriter.WriteLine("ReturnStat");
        //    using (indentingWriter.Indent())
        //    {
        //        indentingWriter.WriteLine("explist... implement"); //TODO: implement
        //    }
        //}
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class TableConstructorNode : SyntaxNode
    {
        [Required]
        readonly Token openCurly;
        readonly FieldList fieldList;
        [Required]
        readonly Token closeCurly;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        //internal override void ToString(TextWriter writer)
        //{
        //    var indentingWriter = IndentingTextWriter.Get(writer);
        //    indentingWriter.WriteLine("TableConstructor");
        //    using (indentingWriter.Indent())
        //    {
        //        FieldList.ToString(indentingWriter);
        //    }
        //}
    }

    [GenerateImmutable(GenerateBuilder = true)]
    public partial class FuncBodyNode : SyntaxNode
    {
        [Required]
        readonly Token openParen;
        [Required]
        readonly ParList parameterList;
        [Required]
        readonly Token closeParen;
        [Required]
        readonly BlockNode block;
        [Required]
        readonly Token endKeyword;

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
    #endregion

}
