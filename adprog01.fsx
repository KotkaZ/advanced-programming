(*

  ITT8060 -- Advanced Programming 2022
  Department of Computer Science
  Tallinn University of Technology
  ------------------------------------

  Lecture 1: Lightning Introduction

  Juhan Ernits

*)

// no boiler plate is required to start coding in  fsharp script file
// (extension .fsx)

// let is the most important keyword in F#


// a simple let, let name = expr, the whole thing is an expression
// VSCode // In the lecture, VSCode is configured to use
// VSCode // inlayHints=offUnlessPressed
// VSCode // which means that to see inlayhints, press Ctrl + Alt. 
// VSCode // To change the setting, press Ctrl+, and type inlayHints.
let text = "All the king's horses and all the king's men"
text
// Assingment / mutation operator will not work
//text <- ""

// let function arg = expr, type annotation,


let splitAtSpaces (text:string)  =  Array.toList (text.Split ' ')

// object method, unix pipe, static method.
// note text here is a bound variable, shadowing the def above
//let splitAtSpaces (text:string) = text.Split ' ' |> Array.toList
let splitAtSpaces (text:string)  = text.Split ' ' |> Array.toList


//an expression, could type in repl with ;;
// val it
splitAtSpaces text

// sequence of let bindings,
// ending with a tuple, a very important type constuctor
// no type annotation needed this time
// note tuple syntax of inferred type
let wordCount text =
  let words    = splitAtSpaces text
  let wordSet  = Set.ofList words
  let numWords = words.Length
  let numDups  = words.Length - wordSet.Count
  numWords, numDups

// we can test immediately again
// note type 
wordCount text

// another function definition
// inner let binds a pattern, a tuple
// next two lines are side effects, function returns no meaningful value
// see type
let showWordCount text =
  let numWords, numDups = wordCount text
  printfn "--> %d words in text" numWords
  printfn "--> %d duplicate words" numDups

//let showStats 

// test immediately, prints two lines and returns unit
showWordCount text
showWordCount "Couldn't put Humpty together again"

// Defining functions with multiple arguments
let squareAndAdd a b = a * a + b
let squareAndAdd (a:float) b = a * a + b
let squareAndAdd (a:float) (b:float) : float = a * a + b




// a few more aspects of let

// used before defined
let badDefinition1 = 
    let words = splitAtSpaces input
    let input = "We three kings"
    words.Length

// used before defined 
let badDefintion2 = badDefinition2 + 1

// shadowing of identifiers, looks like mutation but isn't,
// it is a useful feature.
let powerOfFourPlusTwo n =
    let n = n * n
    let n = n * n
    let n = n + 2
    n

// test immediately
powerOfFourPlusTwo 2

// a bit more about tuples
let site1 = "http://www.cnn.com", 10
let site2 = "http://news.bbc.com", 5
let site3 = "http://www.msnbc.com", 4
let sites = site1, site2, site3

fst site1
snd site1 

let relevance = snd site1

// Note that fst and snd are functions with a single argument.
// The argument if of type 2-tuple or pair.
let fst (a, b) = a
let snd (a, b) = b

// Let can be used to bind several identifiers according to 
// the structure of the value
let url, relevance = site1
let siteA, siteB, siteC = sites

// But when the structure does not match, it will not type.
let a, b = 1, 2, 3

// side effects
let two = printf "Hello World"; 1 + 1
let four = two + two

open System.IO
open System.Net
open System.Net.Http
// task syntax that is new in F# 6.0, previously similar result was achieved with async
// We are going to see later in the course what the differences between task and
// async computation expression syntax are. Here just use the function as is to get the value.
let getHttp (client:HttpClient) (url:string) = 
    task {
        let! response = client.GetAsync(url) |> Async.AwaitTask
        response.EnsureSuccessStatusCode () |> ignore
        let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
        return content
    }

let client = new HttpClient()
let x = getHttp client "https://www.err.ee"
x.Result

let url="https://ws-export.wmcloud.org/?format=txt&lang=et&page=T%C3%B5de_ja_%C3%B5igus_I"
let tõdeJaÕigusI=(getHttp client url).Result




// factorial function
let rec fact n = if n <= 1 then 1 else n * fact (n-1)

let rec fact n = 
   match n with
   | 1 -> 1
   | _ -> n * fact (n - 1)

let s = "Couldn't put Humpty"
s.Length
s[13]
// Until F# version 6 the following indexing was used. 
// now s[13] is the recommended way forward.
s.[13]
s.[13..16]

s[13] <- 'h'

"Couldn't put Humpty" + " " + "together again"

let limit x =
  if x >= 100 then 100
  elif x < 0 then 0
  else x
