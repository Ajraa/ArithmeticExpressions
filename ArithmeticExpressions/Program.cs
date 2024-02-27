using System.Text;

char[] sings = { '*', '+', '-', '/' };


int evaluate(int a, int b, char sign)
{
    switch (sign)
	{
		case '+':
			return a + b;
		case '-':
			return a - b;
		case '*':
			return a * b;
		case '/':
			return a / b;
		default:
			throw new ArgumentException("Unsupported sign");
	}
}



int evaluate_evaluation(string eval)
{
	Stack<int> stack = new Stack<int>();
	Queue<char> queue = new Queue<char>();
	Stack<char> brackets = new Stack<char>();
	bool bracket = false;
	StringBuilder sb = new StringBuilder();
	for (int i = 0; i < eval.Length; i++)
	{
		char c = eval[i];
		if (!bracket)
		{
			if (!sings.Contains(c) && c != '(' && c != ')')
			{
				sb.Append(c);
			}
			else if (sings.Contains(c))
			{

				queue.Enqueue(c);
				if(sb.Length > 0)
				{
					int n = int.Parse(sb.ToString());
					stack.Push(n);
					sb.Clear();
				}
                
			}
			else
			{
				if (c == '(')
				{
					brackets.Push(c);
					bracket = true;
				}
			}

			if ((i == eval.Length - 1))
			{
				int n = int.Parse(sb.ToString());
				stack.Push(n);

				sb.Clear();
			}

			if (stack.Count == 2)
			{
				int b = stack.Pop();
				int a = stack.Pop();
				char s = queue.Dequeue();
				int res = evaluate(a, b, s);
				stack.Push(res);
			}
		}
		else
		{
			if (c != ')')
				sb.Append(c);
			if (c == '(')
			{
				brackets.Push(c);
				bracket = true;
			}
			else if (c == ')')
			{
				brackets.Pop();
				if (brackets.Count == 0)
				{
					stack.Push(evaluate_evaluation(sb.ToString()));
					bracket = false;
					sb.Clear();
				}
				
			}
		}
		
	}

	if (stack.Count == 2)
	{
		int b2 = stack.Pop();
		int a2 = stack.Pop();
		char s2 = queue.Dequeue();
		int res2 = evaluate(a2, b2, s2);
		stack.Push(res2);
	}
	
    if (queue.Count != 0)
		throw new Exception("Invalid eval");
	return stack.Pop();
}


string eval = "-1+1*3";
List<char> tmpEval = new List<char>();
tmpEval.Add('p');
bool par = false;

for (int i = 0; i < eval.Length; i++)
{
	tmpEval.Add(eval[i]);
	if (eval[i] == '+' || eval[i] == '-')
		if (par)
		{
			tmpEval.Add(')');
			par = false;
		}
		else
			tmpEval.Add('p');
	if (eval[i] == '*' || eval[i] == '/')
	{
		par = true;
		for (int j = i; 0 <= j; j--)
		{
			if (tmpEval[j] == 'p')
			{
				tmpEval[j] = '(';
				break;
			}
		}
	}
}

if(par)
	tmpEval.Add(')');

StringBuilder evalBuilder = new StringBuilder();
foreach (char c in tmpEval)
{
	if(c != 'p')
		evalBuilder.Append(c);
}
string cleanEval = evalBuilder.ToString();

try
{
	Console.WriteLine(evaluate_evaluation(cleanEval));
}
catch (Exception e)
{
    Console.WriteLine("ERROR: {0}", e.Message);
}