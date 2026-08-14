using Acornima;
using Acornima.Ast;

namespace Jint.DebugAdapter.BreakPoints;

public class BreakPointCollector : AstVisitor
{
    public List<Position> Positions { get; } = [];

    public override object Visit(Node node)
    {
        if (node is Statement && node is not BlockStatement)
        {
            Positions.Add(node.Location.Start);
        }

        base.Visit(node);

        return node;
    }

    protected override object VisitDoWhileStatement(DoWhileStatement doWhileStatement)
    {
        base.VisitDoWhileStatement(doWhileStatement);

        Positions.Add(doWhileStatement.Test.Location.Start);

        return doWhileStatement;
    }

    protected override object VisitForInStatement(ForInStatement forInStatement)
    {
        base.VisitForInStatement(forInStatement);

        Positions.Add(forInStatement.Left.Location.Start);

        return forInStatement;
    }

    protected override object VisitForOfStatement(ForOfStatement forOfStatement)
    {
        base.VisitForOfStatement(forOfStatement);

        Positions.Add(forOfStatement.Left.Location.Start);

        return forOfStatement;
    }

    protected override object VisitForStatement(ForStatement forStatement)
    {
        base.VisitForStatement(forStatement);

        if (forStatement.Test is not null)
        {
            Positions.Add(forStatement.Test.Location.Start);
        }

        if (forStatement.Update is not null)
        {
            Positions.Add(forStatement.Update.Location.Start);
        }

        return forStatement;
    }

    protected override object VisitArrowFunctionExpression(ArrowFunctionExpression arrowFunctionExpression)
    {
        base.VisitArrowFunctionExpression(arrowFunctionExpression);

        Positions.Add(arrowFunctionExpression.Body.Location.End);

        return arrowFunctionExpression;
    }

    protected override object VisitFunctionDeclaration(FunctionDeclaration functionDeclaration)
    {
        base.VisitFunctionDeclaration(functionDeclaration);

        Positions.Add(functionDeclaration.Body.Location.End);

        return functionDeclaration;
    }

    protected override object VisitFunctionExpression(FunctionExpression function)
    {
        base.VisitFunctionExpression(function);

        Positions.Add(function.Body.Location.End);

        return function;
    }
}

