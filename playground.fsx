(* PREPARATION

Before you start, it's worth knowing about the Elmish architecture
See the overview slide.

Also these basic forms, see cheat sheet

  let v = expr                       -- Define a value
  let mutable v = expr               -- Define a mutable value

  let f arg1 arg2 = expr             -- Define a function

  f arg1 arg2                        -- Call a function
  arg |> f                           -- Pipeline call a function
  (fun arg1 arg2 -> expr)            -- Function value (lambda)

  v.Name                             -- Get a property
  v.Method(arg1, arg2)               -- Call a method

  "abc"                              -- string
  $"abc {1+1} def"                   -- interpolated string
  $"abc %d{1+1} def"                 -- strongly typed interpolated string

  (1, "two")                         -- tuple value
  Some (4+4)                         -- option value
  None                               -- option value

  [ 1; 2 ]                           -- list expression
  [ expr ]                           -- list expression (computed)

  async  { expr }                    -- async expression
  let! v = expr                      -- await in async expression
  return expr                        -- result of async expression

  match expr with                    -- pattern match
  | pat1 -> expr1
  ...
  | patN -> exprN

  if expr then                       -- conditional expression
      expr

  if expr then                       -- conditional expression
      expr
  else
      expr

  for v in expr do                   -- loop expression
      expr

  type R =                           -- record type
      { Field1: type1
        ...
        FieldN: typeN  }

  type U =                           -- union type
     | Case1 of type1 * ... * typeN
     ...
     | CaseN of type1 * ... * typeN

  type C(arg1,...argN) =                     -- class type
     let ...
     <members>

     member this.Property = expr             -- a property on a type
     member this.Method (arg1, arg2) = expr  -- define a method on a type
*)


// Task 5 - play with a new web service using F# scripting

// Task 5.1 -
//   Problem: Learn to use the F# REPL
//
//   Approach
//   - Type 1+1
//   - Select the code (keyboard or mouse)
//   - Use "Alt-Enter" to execute in REPL


// Task 5.2
//   Problem: we want to examine the REST service we would like to use
//
//   Approach:
//     Look at https://www.geonames.org/export/ws-overview.html
//     Look at http://api.geonames.org/findNearbyWikipediaJSON?lat=52.3676&lng=4.9041&username=dsyme



// Task 5.3
//   Problem: We need a package to use
//
//   Look at https://nuget.org, search for FSharp.Data
//   Copy the text to reference for scripting.


// Task 5.4 - Point the JsonProvider to the web service
//

// open FSharp.Data
// type WikipediaIO = JsonProvider<"http://api.geonames.org/findNearbyWikipediaJSON?lat=52.3676&lng=4.9041&username=dsyme">


// Task 5.5 - Use the JsonProvider to download the data, the code is below
//
// let info =
//     $"http://api.geonames.org/findNearbyWikipediaJSON?lat=52.3676&lng=4.9041&username=dsyme"
//     |> WikipediaIO.AsyncLoad
//     |> Async.RunSynchronously



// Task 5.6 - Extract the title, latitude, longitude and summary for each result
//
// A starting snippet is below

// [ for x in info.Geonames -> x.Title ]



