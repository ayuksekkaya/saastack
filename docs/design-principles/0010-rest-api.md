# REST API Design

Q. How is our web API to be designed?

A. **REST-fully** modeling real-world processes as much as we can, with a sprinkling of RPC as little as we can.

## Design Principles

> We are going to be following conventions (not standards) around RESTful web service design. But we also recognize that in some cases, remote actions need to be presented as RPCs to make them more intuitive to our users (Developers).

The primary influence of these design principles has been these:

* [RESTful Best Practices](https://github.com/tfredrich/RestApiTutorial.com/raw/master/media/RESTful%20Best%20Practices-v1_2.pdf)

* [REST API design and escaping CRUD](https://www.thoughtworks.com/insights/blog/rest-api-design-resource-modeling), from Thoughtworks.com

The goal is to adopt REST principles as far as they make sense, and at the pace we can adopt them.

Level 3 of the [Richardson Maturity Model](http://restcookbook.com/Miscellaneous/richardsonmaturitymodel/) is sufficient for us right now.

> The key point is that, no matter what design decision we make, whether it is right/wrong best practice or just what you like most, the main principle that over-arches all others is to be consistent (right across the codebase) and remain highly maintainable and testable at all times.

### REST versus CRUD

Even though most web APIs are defined by the HTTP methods: `POST`, `GET`, `PUT`, `PATCH`, `DELETE` (and others),

    - AND these methods *could be* conveniently translated nicely into `Create` `Retrieve`, `Update` and `Delete` (CRUD) functions of a database. 
    
    - AND given that REST is designed around a "Resource", each with an identifier. 

Designing a REST API for web interop is not to be confused with designing a database API with CRUD.

> REST and CRUD are two different design styles for two different parts of the system that ultimately must mesh together at some layer in the code. But keeping them separate is important for increasing the usability of your API.

#### A little history about CRUD

The tendency for modern web developers to model their web APIs with familiar CRUD patterns comes from a long history and legacy of data-modeling practices that are still very common, even today.

The quickest way (read: *"the least amount of code to
write"*) of creating and updating data in a system, is to build a thin API interop layer directly over the top of a database.

##### Reuse

Once relational databases (RMDBS) were invented (1970-1990s) and then later object-relational mappers (ORMs) were invented (2000s) to communicate with those databases, the code to provide such an API was trivial and so predictable, that it could have been generated by tooling automatically.

> Beyond the 2000s, the tooling has become so good that we actually no longer needed developers to write that kind of code. But today so many still do, just for the fun of it, so it seems.
>
> It also seems evident that if a developer has only ever \[professionally\] built software using a relational database, they have no other tools than ORMs to access any data. Today, relational databases are not the only tools to store data, and ORMs are not the only tools to access it.

With RMDBS and ORMs being so prevalent, developers building CRUD systems only really then need to focus on two things:

1. Naming and data type selection of columns in a SQL database (and applying indexes in the right places)
2. The relationships with other tables, using foreign keys.

Normalization dictated that duplicity of data should be eliminated, and thus, reuse of data to be maximized. Thus, we end up with simple relationships and multi-variant semantics crammed into the most efficient datatypes for minimizing storage space, and maximizing performance.

This mantra of "data reuse" was then poorly extrapolated out and over-applied to reuse data across unrelated components in the system. In many medium-sized systems of this kind, developers would start mapping new use cases (for new components) onto existing tables that were designed for other components of the system. Semantics and abstractions were mixed. Worse, these existing tables were then modified for newer use-cases for newer components of the system, breaking existing functionality in a way very hard to detect before releasing the software.

In the blind pursuit of not duplicating any data in a database, developers were treating the entire database as having "global scope". Thus, no segregation existed between the data of unrelated components in the system. After a few hundred tables in the database and years of tangled development it became impossible to later split up the tables into separate databases. Since it was too hard to figure out (from the code) what components and use cases were coupled to what database tables. This resulted in big balls of mud (BBOM) codebases stagnating and dying after only a few years, as no one wanted to break them. The only thing worth keeping (after the fact) is the dataset itself.

##### Performance

Back in the 1990s, databases were generally very poorly performant for most complex systems due to storage medium read/write speeds, and data storage at the time was also very expensive, and unreliable. Performance (of queries) in relational databases also started to decline when those systems started to grow in complexity. It became critical to success for developers to take more care about how rows and columns in tables fit into single [tracks into sectors](https://en.wikipedia.org/wiki/Disk_sector) on a magnetic hard drive, to speed up retrieval of queried data. Far more than it mattered about how the system actually worked. Needless to say, usability was not really such a big focus back then. CRUD based UXs were, (and unfortunately still are) common.

> Magnetic hard drive: a hard drive with physical spinning disks. Long before SSDs were invented.

Because reducing the amount of data stored was a sure way to increase storage access times, developers then focused on cramming as much meaning/semantics/variability into the smallest number of bytes of data (and using fancy encodings) to fit that data into the smallest length database column as possible. `nvarchar(max)` was considered a performance smell back then.

Today, SSD disks are (at least) an order of magnitude faster than spinning disks ever were, and most of these physical "disks" are now hidden so far away from developers (in the cloud), that it no longer matters for the design of most SaaS systems in their first few years.

The days of caring about these kinds of database optimizations early in the development of a software product are long gone.

> Design Principle: If you design your system to be modular at the start, then you can optimize the individual parts of it to be far more performant later, once you know these things: (a) that this module is critical to the success of the system, and (b) that this module works well and is stable, and (b) that any specific optimization for this module has a predictable ROI for optimizing it. Three things you cannot know about a module, ahead of building it.

### Modeling REST resources

Real-world SaaS products today are \[necessarily\] more complex than the CRUD systems of the 1990s. As such CRUD, whilst having its
*niche* place in software development (i.e. for very simple systems centered on a database), is not a very useful pattern for defining web-based APIs anymore.

Creates and updates in CRUD are far too generalized. To be generalized, they must assume that their clients (the consumers of the API) must have more knowledge of the details of the underlying constraints of the system - than they reasonably should have. It is essentially a usability problem.

> More generalized -> increased scope of reuse -> less usable/optimal/specific in any given context

A REST API on the other hand is striving to model actual and very specific real-world processes, far closer to the mental models of the clients using the system. Thus, if you understand the real world (upon which the API is based), and you understand some basics of the constraints of the system underlying it, then a specific API can easily document and enforce those constraints just for that specific context.

> REST APIs are not striving to "project" the semantics of tables of relational databases across the internet, and expecting the consumers to care about those semantics.

Thus, REST "resources" are designed to be the *nouns* involved in the
*state* changes of those real-world business processes or concepts, decorated with specific "actions" being the
*verbs* operating on those processes or state changes.

> Rather than being the *nouns* of the optimized data being stored in normalized relational database tables.

There is no assumption anywhere in a software system today that a REST API will persist its
*state* in a relational database, let alone a single consistent one (like we had to have back in the 1990s). Far from it, many REST APIs actually store their [aggregated] state across the stores of distributed systems, where individual pieces of state may or may not reside in any database of any kind.

See [this excellent piece](https://www.thoughtworks.com/insights/blog/rest-api-design-resource-modeling) on what you should be modeling in your REST APIs.

## Implementation

### Home URLs

The home URL for any service will be in the form:

    https://api.productname.com/{version}/{resource}

where `{version}` is optional (typically of the form: `v3`)
where `{resource}` is the pluralized name of the resource

### Resource Base URLs

2 base URLs for each resource: one for collections, the other for elements

For example:

`/cars`   for several cars (a collection)
`/cars/{carid}`  for a specific car (an element)

> Resource names are always lower cased, and pluralized

Multi-word resources use a hyphen in the name

For example:

`/delivery-vans` for several delivery vans (a collection)

### Method Schemes

There are a number of popular schemes for REST APIs today.

> They are designed around discoverability and usability.

#### GET-POST-DELETE

Simple scheme. Only three HTTP methods, to cover all possible actions.

Use `GET` to fetch a resource, or fetch a collection of resources

Use `POST` to create a new resource, and for changing an existing resource, and to execute any action on the resource

Use `DELETE` to delete a resource or for any reductive action on the resource (i.e. "cancel" a process)

> For example, the Stripe API

#### GET-POST-PUT-DELETE

Here, we add the `PUT` method for changing to any existing resources.

Use `GET` to fetch a resource, or fetch a collection of resources

Use `POST` to create a new resource

Use `PUT` to change an existing resource, and to execute any action on the resource

Use `DELETE` to delete a resource or for any reductive action on the resource (i.e. "cancel" a process)

#### GET-POST-PUT-PATCH-DELETE

Here, we add the `PUT` or the `PATCH` method for changing any existing resources.

Use `GET` to fetch a resource, or fetch a collection of resources

Use `POST` to create a new resource

Use `PUT` or `PATCH` to change an existing resource, and to execute any action on the resource

Use `DELETE` to delete a resource, or for any reductive action on the resource (i.e. "cancel" a process)

This is the current scheme in use for this API.

### Methods

* No methods are permitted in the URL.

* We only support the following HTTP methods: `GET`, `POST`, `PUT`, `PATCH` and `DELETE`

For a collection resource:

* `GET` = list all elements in a collection
* `POST` = creates a new resource
* `PUT `= N/A
* `PATCH` = N/A
* `DELETE` = N/A (very rare)

For an element resource:

* `GET` = view a resource or view a collection of resources
* `PUT` or `PATCH` = execute an action, and update resource (wholly or partially)
* `POST` = N/A
* `DELETE` = delete a resource

For examples:

    GET /cars - lists all cars (can be a search with filters or a batch get)
    GET /cars/1234 - returns the car 1234
    PATCH /cars - N/A
    PATCH /cars/1234 - updates car 1234
    PUT /cars - N/A
    PUT /cars/1234/publish -  publishes car 1234
    POST /cars - creates a new car
    POST /cars/1234 - N/A
    DELETE /cars - N/A
    DELETE /cars/1234 - deletes car 1234

### Nouns

We always pluralize the element base URL:

    /cars/{Id}

### Associations

We chain associations (i.e. `ObjectA` has `ObjectB`)

For example, if a `car` that **has an** `availability`, and that `availability` **has** a `timeslot`:

* Fetch a list all the `timeslots` for that `availability`:

  `GET /cars/{carid}/availabilities/{availabilityid}/timeslots`

* Create a new `timeslot` for that `availability`:

  `POST /cars/{carid}/availabilities/{availabilityid}/timeslots/{timeslotid}`

Alternatively, depending on if `availabilities` are considered themselves a top-level resource, then you may also have an API for `availabilities` in their own right, which makes the relationship above redundant (in most cases):

* Fetch a list of all timeslots for that `availability`:

  `GET /availabilities/{availabilityid}/timeslots`

* Create a new `timeslot` for that `availability`:

  `POST /availabilities/{availabilityid}/timeslots/{timeslotid}`

### Embedding Resources

Generally, it is a violation of REST to _expand_ associations in representations by default, as this can lead to `N+1` problems in distributed systems, and there have to be practical limits on how deep the resources should expand.

> Consider what should happen with cyclic resources

For example, a `car` resource has a `useraccount` resource for the owner of the `car`, and it also has a `membership` to an `organization` resource.

REST strictly says that the `useraccount` resources **should not** be included (embedded) in the representation of the `car` resource.

The resources are separate top-level resources. But if they are associated then the `useraccount` of the `car` should only be referenced by its ID.

> The [HAL specification](https://en.m.wikipedia.org/wiki/Hypertext_Application_Language) is one example of how to define embedded resources, with its `_embedded` pattern.

The constraint in REST forces a client to explicitly request any embedded resources explicitly, if the client requires them, and to aggregate them if necessary.

In practice though, this alleviates only some of the problems of the
`N+1` problem on the server, by now delegating that problem to the client, and then, even only in some cases.

In other cases, fetching embedded resources is desirable for the client, and in some cases, it is undesirable for the client, it depends on the client, and what they want to do.

Embedding (by default) **will always
** eventually lead to performance problems, mostly in the network latency fetching these embedded resources by either client or server, and the
`N+1` problem is prevalent in these cases, especially if collections are being embedded.

The only way to tackle all of these problems is for the client to have fine-gained full control over what the server must provide it. This approach is very difficult or impractical to implement at scale.

To address some of the more common problems, the following general guidelines should be followed:

#### Single Resource Requests - Embed by default

When requesting a single resource that has embedded relationships, then the embedded resources **can and should be fully
expanded**, <u>unless</u> the client overrides with explicit expansions or no expansions.

This policy generally applies to any `GET`, `PATCH`, `PUT`, `POST` or `DELETE` request that returns a single resource.

For example:

    GET /cars/{carid}

Should return a car with its embedded resources for the owner and the feedback.

```
200 - OK
{
    "id": "car1",
    "owner": {
        "id": "user1",
        "name": "aname",
        ...
    },
}
```

> In general, this rule requires that the server must request all embedded child resources from other services, but that should be relatively limited unless the resource has any aggregations. In those cases, the server may choose whether to embed the aggregations or not, and the client can also influence that with the
`?embed=off`.

#### Collection Resource Requests - Do Not Embed by default

When requesting a collection of resources (e.g. in a search or list API) then the embedded resources **should not be
expanded by default**, <u>unless</u> the client overrides with explicit expansions, or all expansions.

This policy generally applies to any `GET` request that returns a collection of resources.

For example:

    GET /cars?orderby=name

Should return all cars without embedded resources for the owner.

```
200 - OK
[
    {
        "id": "car1",
        "owner": {
            "id": "user1",
        },
    },
    {
        "id": "car2",
        "owner": {
            "id": "user2",
        },
    }
]
```

> In general, this rule eliminates the
`N+1` problem on the server, but requires that the client must fetch all child resources from other services if needed. In that case, the client may choose to opt in to the server doing the embedding with the
`embed=resource.childresource` parameter, or the child requests the other resources itself.

#### Overriding Defaults

When requesting a collection resource OR requesting a single resource, the following override options are supported to override the default policy:

- `embed=off` do NOT expand **any** embedded resources. (This is the default for all collection resource requests).
- `embed=*` expand **all** embedded resources. (This is the default for all single resource requests.)
-
`embed=resource.childresource.grandchildresource,resource.childresource` expands only the specified resource properties. (e.g. only
`resource.childresource.grandchildresource` and `resource.childresource`)

#### A Network Of Data

As a side note. In some REST domains, there is something that can be imagined as "a network of data", which is made up of all the cached resources previously requested by clients. That is, of both the services and the clients implement HTTP caching correctly.

The network is dynamic in nature and its size and effectiveness are determined by how long resources remain in the network (cache). Over time, resources are updated and flushed out of the cache or expire from the cache. So the size and availability of this network is largely related to how much use of the network there has been by any specific client and other clients who stimulate resources being cached in the network.

The
`N+1` problem mentioned in the Embedding section above, is alleviated somewhat by the presence of the network of data, and dependent largely on how many services are providing resources to the network.
The network of data will not reside within the bounds of a single service, nor between resources that are served by the same service, since its unlikely that those in-process method calls are cached in the HTTP caches of the network of data.
However, outside the services, and as services are separated (i.e. Move towards becoming microservices), the network of data starts to have more of a performance benefit to the
`N+1` problem.

### Searches & Filtering

Use query string properties for limiting, sorting, and filtering on properties of resources.

These properties generally don't form part of the route.

In the API the naming convention for search type API's has been the following:

* For search type API's, where the request contains something to search for, (even in cases where only a single result is expected) we have been using the convention: `GET /resources/search`, and defining the search criteria in the QueryString. OR you could define a specific kind of search if you have to distinguish between searches:
  * For example, to determine if a user exists for a specified email address, we have the SearchUsers API: `GET /users/search?email=bob@company.com`.
  * For example, you have several search methods each one quite distinct, you could use: ` GET /users/byname` and `GET /users/byid` each with other filters in the query.
  
* For Listing type API's, where the request may not contain any search criteria, and usually returns different variants of a resource based upon the caller, or context, we have been using the convention: `GET /resources`, and defining any parameters in the QueryString also.
  * For example, to list the car that you own, we have the ListForCallerCars API: `GET /cars`.

The difference in the naming convention is purely for semantics. For search APIs, the route adds the `/search` part, or other distinguishing action `/byid`.

#### Search Metadata

In all cases (Search-type API's, and List-type API's), the request will support ([IHasSearchOptions](../../src/Infrastructure.Web.Api.Interfaces/IHasSearchOptions.cs)) filter, sorting, and pagination parameters, the results will contain Metadata about the actual results being returned. ([SearchMetadata](../../src/Application.Interfaces/SearchResultMetadata.cs))

For example, a call to
`GET /cars?sort=-LastModifiedUtc&limit=50&offset=100` will include the following metadata in its request:

```
GET /cars?sort=-LastModifiedUtc&limit=50&offset=100
```

and the following in its response:

```
200 - OK
{
    "cars": [...],

	"metadata": {
        "total": 172,
        "limit": 50,
        "offset": 100,
        "sort":{
            "by": "LastModifiedUtc",
            "direction": "Descending",
        "filter": {},
        "distinct": "",
        }
    },
}
```

#### Searching

Search APIs use the `/search ` action.

For example, for a car that has a color property:

* Fetch a list of all red cars:

  `GET /cars/search?color=red`

There are some cases where you may want to include the filter in the route itself for common searches. These require careful design because the routes cannot be variable over time.

For example, for images, of a specific (system-wide, and well-known) size:

* Fetch a list of all thumb-sized images:

  `GET /images/thumb`

#### Filters

To limit the actual data sent over the wire.

> Useful for connections of reduced bandwidth.

When you want to limit the data that comes back over the wire.

For example, to tell the API not to send this data over the wire:

```
?filter=field1;field3
```

#### Pagination

For limiting search results and for paging results.

> Most commonly available on search APIs only

* An `offset` (or start) for where to begin the result set (zero-based)
* A `limit` (or count) for how many results to return for each page

For example, to fetch only the first page of results (of a page containing 50 results):

```
?offset=0&limit=50
```

For example, to fetch the 3rd page of results (of a page containing 50 results):

```
?offset=100&limit=50
```

> By default, a limit of -1 or 0 means no limiting, and an offset of 0 means no offset.
>
> The default limit, and maximum of all search results will be 100, by default.
>
> An offset of 0 means the first page of results.

#### Sorting

For sorting results (usually prior to limiting).

> Most commonly available on search APIs only

* A `sort` (or field name) to sort on
* An `order` (or direction) to sort in

For example, to sort and then order the results:

```
?sort=field1&order=asc
```

> In practice, both can be accomplished in the same syntax: i.e.
`&sort=-field1` to sort descending on field name 'Field1'

#### Distinct

For limiting the results (usually prior to limiting and sorting), on a given field

* A `distinct` (or field name) to be distinct on

For example, to ensure no duplicate names of cars:

```
?distinct=name
```

### Versioning

* Avoid versioning if at all possible.

* If necessary, then be explicit about the version in the base URL

For example,

    /api/<dateornumber>/noun

> Can use the date of release as the version number, or the version number itself.

### Response Formats

The service will return JSON responses by default.

It will **try** to provide responses in formats that the client typically requests in the `Accept` header

> Other formats (e.g. CSV, SOAP, XML, etc.) may be supported as needed.

We support the following means to request different content types:

1. Using the HTTP Accept Header (i.e. `Accept: application/json`)
2. By appending the filetype (i.e. `.json`) suffix to the URL (i.e. `/cars.json`)
3. Appending the format to the query string: `?format=json` (i.e. `/cars?format=json`)

### Response Bodies

All response bodies include bodies in an "enveloped" response.

#### Successes

For example, to fetch a list of `cars` (in JSON):

```
GET /cars
200 - OK
{
    "cars": [
        {
            "id": "car1",
            "color": "red"
        },
        {
            "id": "car2",
            "color": "blue"
        }
    ]
}
```

For example, to fetch a specific `car` (in JSON):

```
GET /car/car2 
200 - OK
{
    "car": {
        "id": "car2",
        "color" : "blue"
    }
}
```

The envelope may also contain other nodes, as well.

For example, to fetch a list of `cars`, the envelope will also contain an extra node for the search `metadata`:

```
GET /cars
200 - OK
{
    "cars": [
        {
            "id": "car1",
            "color": "red"
        },
        {
            "id": "car2",
            "color": "blue"
        }
    ],
	"metadata": {
        "total": 172,
        "limit": 50,
        "offset": 100,
        "sort":{
            "by": "LastModifiedUtc",
            "direction": "Descending",
        "filter": {},
        "distinct": "",
        }
    },
}
```

#### Errors

Error responses (all `4XX` or
`5XX`) responses, will include an [RFC7808](https://datatracker.ietf.org/doc/html/rfc7807) error in them.

For example, a `500 - Internal Server Error` would look like this:

```
POST /users 
{
    "color":"mauve"
}
500 - Internal Server Error
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.6.1",
    "title": "An unexpected error occurred",
    "status": 500,
    "detail": "amessage",
    "instance": "http://localhost/testingonly/throws",
    "exception": "System.InvalidOperationException: amessage"
}
```

For example, a `400 - Bad Request` (from a failed validation) might look like this, with additional information:

```
POST /cars/car_12345678901 
{
    "year": 2023
}
400 - BadRequest
{
    "type": "NotEmptyValidator",
    "title": "Validation Error",
    "status": 400,
    "detail": "'Color' must not be empty.",
    "instance": "http://localhost/cars/car_12345678901",
    "errors": [
        {
            "rule":"NotEmptyValidator",
            "reason": "'Make' must not be empty.",
            "value": "null"
        },
        {
            "rule":"NotEmptyValidator",
            "reason": "'Model' must not be empty.",
            "value": "null"
        }
    ]
}
```

### Response Codes

#### Successes

We report successful requests as HTTP status codes `2XX`.

These are the common HTTP status codes for success:

* `GET`: `200 - OK`, or `204 - NoContent` or `206 - PartialContent`
* `POST`: `201 - Created`, or `202 - Accepted`
* `PUT/PATCH`: `202 - Accepted`
* `DELETE`: `202 - Accepted`, or `204 - NoContent`

> In addition, when creating a new resource, we also will be setting the 'Location' header to be the URL to GET the new resource.

> HTTP Status codes explained here: [HTTP Status Codes](http://en.wikipedia.org/wiki/Http_error_codes)

For example, callers should expect appropriate response bodies for the following endpoints:

* `GET /cars/car1` should return a representation of the  `car` in a `200 - OK` response
* `GET /cars` should return a representation of all the `cars` (and `metadata`)  in a `200 - OK` response
* `PUT /cars/car1` should return a modified representation of the `car` in a `202 - Accepted` response
* `PATCH /cars/car1` should return a modified representation of the `car` in a `202 - Accepted` response
* `POST /cars` should return the created representation of the `car` in a `201 - Created` response
* `DELETE /cars` should return no content in a `204 - NoContent` response

> Note: In distributed systems, the resource representation in the response may not include fully consistent values due to downstream asynchronous processes. Consistency may be eventual, and up-to-date resources may need to be fetched later.

#### Errors

We report errors as HTTP status codes `4XX` and `5XX`).

These are the common HTTP status codes for errors:

* `400 - BadRequest` (the request is incorrectly formatted)
  * Validation failed
  * A required input is missing or invalid at the time
  * The request is not allowed at this time because the resource/context is not in a required state - (i.e. business rule violation).
* `401 - NotAuthorized` (the user has not authenticated when authentication is required)
  * No access_token provided for a secure call
  * The access_token expired or is invalid
* `402 - PaymentRequired` (the user is using a feature that has not been paid for)
  * The caller is using a feature associated to a subscription plan that the user does not have
* `403 - Forbidden` (the user may be authenticated, but they are not authorized to this specific resource at this time)
  * The caller is not in the required role
  * [May decide to throw 404 instead to obscure reason from hacker]
* `404 - NotFound` (a resource does not exist)
  * The resource does not exist
  * [May decide to throw 404 instead of a 403 if the caller is not allowed access to this resource to obscure the resource rather than admit it exists but the authenticated user does not have access to it]
* `405 - MethodNotAllowed` (Method/API cannot be called at this time)
  * i.e. It is invalid to call it for this resource at this time.
  * i.e. It no longer exists for this resource at this time, or it never existed for this resource (not implemented yet).
* `409 - Conflict` (conflict with the current state of the target resource)
  * Resource already exists
* `423 - Locked` (the target resource is locked)
  * Resource is locked
  *
  `500 - InternalServerError` (something bad happened in our code that we did not expect, and did not handle in the code)
  * Unhandled/Unexpected exception (not covered above)

> HTTP Status codes are explained in detail here: [HTTP Status Codes](http://en.wikipedia.org/wiki/Http_error_codes)