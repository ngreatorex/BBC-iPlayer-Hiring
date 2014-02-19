
# Coding Exercise

Your team has been asked to create an Agile/Scrum task tracking system. This web system will allow the entire BBC to use this tool to manage their team’s backlog, and every employee will have access to it. 

You are part of the backend web service team, and are responsible for implementing a Backlog object that implements the following interface:

```java
public interface IBacklog {
   public void Add(Story s);
   public Story Remove(String id);
   public List<Story> getSprint(int totalPointsAchievable);
}
```

where Story is defined as:

```java
public class Story {
   public String Id;
   public int Points;
   public int Priority;
   ... // The actual content of the Story is omitted here
}
```

- Each Story class has a `Points` instance variable, which represents an estimate of development effort required to complete the story, and a `Priority` instance variable, which represents the business priority of the story.

- The `getSprint(totalPointsAchievable)` method of the Backlog class should return a list of `Story` in the order of business priority, based on the number of points each story takes and the given `totalPointsAchievable` in a sprint.

- You need not implement the entire system or build a UI, but your solution should demonstrate the key functionality specified in the `IBacklog` interface above.

- You may implement your solution in any appropriate (i.e. non-obscure) language, and you may choose to implement the storage of any relevant data structures in memory or using a database, but please make sure your solution is working end-to-end and you include instructions for us to run and test your code.

- You should consider how concurrent use of the system may impact your implementation.

- Your response will be evaluated against four key criteria:
  - Design, Modularisation and Componentisation
  - Testing Approach and Scenarios
  - Concurrency and Performance
  - Style and Readability