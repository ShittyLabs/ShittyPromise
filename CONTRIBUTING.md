# Contributing to ShittyPromise

1. Read the [Code of Conduct](./CODE_OF_CONDUCT.md)
1. If adding a new feature, follow the [new feature instructions](#adding-a-new-feature)

## Adding a new feature
1. If an issue exists for this method, add a comment to let everyone know that you are implementing it. This helps us avoid duplicates.
1. Fork the repo if you haven't done so already.
1. Create a branch from an up-to-date `develop` branch.
1. Implement the feature.
1. Document the method using standard XML documentation.
1. [Add tests](#adding-a-set-of-tests).
1. Push branch to your fork.
1. Create a pull request against the `develop` branch of the main repo.

## Adding a set of tests
1. Add a file at `./ShittyPromise.Tests/<FEATURE>Tests.cs`.
1. Add tests for success conditions.
1. Add tests for failure conditions.
