using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacklogTracker.Implementation
{
    /// <summary>
    /// This class will generate sprints by treating the problem as the 0,1-knapsack problem
    /// 
    /// This implementation will calculate value such that one story of a higher priority is always
    /// more valuable than every story in the next priority down
    /// </summary>
    /// <remarks>This class IS NOT thread safe</remarks>
    public class KnapsackProblemSolverSprintGenerator : ISprintGenerator
    {
        protected int maxPriority;
        protected int maxCountPerPriority;

        protected long CalculateValue(IStory story)
        {
            return (int)Math.Pow(maxCountPerPriority + 1, (maxPriority - story.Priority));
        }

        protected long[,] GenerateTable(int max, IStory[] candidates, int maxPriority)
        {
            long[,] table = new long[candidates.Length+1, max+1];

            //for i from 1 to n do
            //  for j from 0 to W do
            //    if w[i] <= j then
            //      m[i, j] := max(m[i-1, j], m[i-1, j-w[i]] + v[i])
            //    else
            //      m[i, j] := m[i-1, j]
            //    end if
            //  end for
            //end for

            for (int i = 1; i <= candidates.Length; i++)
            {
                for (int j = 0; j <= max; j++)
                {
                    var candidate = candidates[i - 1];
                    if (candidate.Points <= j)
                    {
                        table[i, j] = Math.Max(table[i - 1, j],
                            table[i - 1, (j - candidate.Points)] + CalculateValue(candidate));
                    }
                    else
                    {
                        table[i, j] = table[i - 1, j];
                    } 
                }
            }

            return table;
        }

        protected IEnumerable<IStory> TraceSolution(int max, IStory[] candidates, long[,] table)
        {
            List<IStory> solution = new List<IStory>();
            int row = table.GetLength(0) - 1;
            int col = table.GetLength(1) - 1;

            long valueRemaining = table[row,col];

            // Walk backwards through the table until we have reached the end, or we have no story points left
            while (valueRemaining > 0 && row > 0)
            {
                // If in this step we added value then add the selected story to the solution
                if (table[row,col] != table[row - 1, col])
                {
                    //Must subtract 1 because row is 1 based and candidates is 0 based.
                    IStory nextStory = candidates[row - 1];
                    solution.Add(nextStory);

                    int weight = nextStory.Points;
                    long value = CalculateValue(nextStory);

                    //Move one row up and the number of columns equal to the weight left.
                    row--;
                    col -= weight;

                    valueRemaining -= value;
                }
                else
                {
                    // We didn't add value in this step, so simply move one row up
                    row--;
                }
            }

            // The solution was generated backwards, so we need to reverse it before returing
            solution.Reverse();

            return solution;
        }

        public IEnumerable<IStory> Solve(int capacity, IEnumerable<IStory> candidates)
        {
            if (capacity < 0)
                throw new ArgumentException("Argument must be greater than zero", "capacity");

            if (candidates == null)
                throw new ArgumentNullException("candidates");

            IStory[] candidateArray = candidates.OrderBy(x => x.Priority).ThenBy(x => x.Points).ToArray();
            maxPriority = candidates.Max(x => x.Priority);
            maxCountPerPriority = candidates.GroupBy(x => x.Priority).Max(y => y.Count());

            long[,] table = GenerateTable(capacity, candidateArray, maxPriority);
            return TraceSolution(capacity, candidateArray, table);
        }
    }
}
