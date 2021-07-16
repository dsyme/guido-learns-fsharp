(* TASK 0 *)

// Before you start, it's worth knowing about the Elmish architecture
// See the overview slide.

// Also these basic forms, see cheat sheet
//
//   let v = expr                       -- Define a value
//
//   let f arg1 arg2 = expr             -- Define a function
//
//   f arg1 arg2                        -- Call a function
//   arg |> f arg1                      -- Pipeline call a function
//   (fun arg1 arg2 -> expr)            -- Function value (lambda)
//
//   v.Name                             -- Get a property
//   v.Method(arg1, arg2)               -- Call a method
//
//   "abc"                              -- string
//   $"abc {1+1} def"                   -- interpolated string
//   $"abc %d{1+1} def"                 -- stringly typed interpolated string
//
//   Some (4+4)                         -- option value
//   None                               -- option value
//
//   [ 1; 2 ]                           -- list expression
//   [ expr ]                           -- list expression (computed)
//
//   async  { expr }                    -- async expression
//   let! v = expr                      -- await in async expression
//   return expr                        -- result of async expression
//
//   match expr with                    -- pattern match
//   | pat1 -> expr1
//   ...
//   | patN -> exprN
//
//   if expr then                       -- conditional expression
//       expr
//
//   if expr then                       -- conditional expression
//       expr
//   else
//       expr 
//
//   for v in expr do                   -- loop expression
//       expr
//
//   type R =                           -- record type
//       { Field1: type1
//         ...
//         FieldN: typeN  }             
//
//   type U =                           -- union type
//      | Case1 of type1 * ... * typeN  
//      ...
//      | CaseN of type1 * ... * typeN  
//
//   type C(arg1,...argN) =                     -- class type
//      let ...
//      <members>
//
//      member this.Property = expr             -- a property on a type
//      member this.Method (arg1, arg2) = expr  -- define a method on a type


(* TASK 1 *)

// You've learned or seen all of the following: 
//   1. let let let let (function definitions and values)
//   2. match for pattern matching and strings
//   3. dot notation. F# supports object programming
//   4. strings, including interpolated strings
//   5. F# is strongly typed.  The IDE knew your types and checks on the fly
//   6. F# knows how symbols resolve: rename, goto-definition etc.

(* TASK 2 *)

// You've learned or seen all of the following: 
//   1. In this programming model, display views are
//      functional data (lists, views). 
//   2. The view is recalculated on each message in the IDE and
//      an incremental diff applied to the actual DOM.
//   3. The functional data can by computed using computed list expressions.
//      This is a super-powerful form of list comprehensions
//      and is based on a very general feature called F# computation expressions.
//      Computed list expressions are incredibly performant and versatile
//   4. You've also seen Tuples (of latitude and longitude) and used
//      helper functions to make markers.

(* TASK 3 *)

// You've learned or seen all of the following: 
//   1. "let and type all day long" 
//   2. record types - a basic workhorse of cheap and cheerful functional data 
//   3. async programming (async expressions) for server requests
//   4. strongly typed string interpolatation 

(* TASK 4 *)

// You've learned or seen all of the following: 
//   1. discriminated union types - here for messages in a web UI 
//   2. pattern matching and completeness checks
//   3. dispatching a new message in the Elmish architecture

#r "nuget: FSharp.Data, 4.1.1"

//FSharp.Data.
