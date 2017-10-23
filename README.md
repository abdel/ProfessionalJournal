# Professional Journal
An immutable journal for professional engineers.

# Azure Web Backend
- [ProfesionalJournalServer](https://github.com/abdel/ProfessionalJournalServer)

# Requirements
- [Visual Studio / Xamarin](https://www.xamarin.com/visual-studio)
- Android SDK 25+
- iOS SDK 10.2+

# Basic Setup
- Clone the repository: `git clone https://github.com/abdel/ProfessionalJournal.git`
- Open the project in Visual Studio

# Contributing
1. Create a new branch using `git checkout -b BRANCH_NAME` where `BRANCH_NAME` is the name of the feature you're working on. For example, if your task you're working on is "Add Entry", then an optimal branch name would be `add-entry`. The branch name should be informative so other team members can know what you're working on.

2. Make changes to the code
3. Once you've made changes to the code, you can either add all files to the "stage" commit or select the files you want to commit using: `git add .` for all files, or `git add FILENAME` for a specific file.

4. Once you added the files you want to commit, run the following command `git commit -m 'MESSAGE'` where `MESSAGE` is an informative message about the updated files. For example, if you finished a part of the task, like "Creating the form to add entry", that could be your commit message.

5. Once changes are comitted, you can push the changes to your branch using `git push origin BRANCH_NAME`. You can then create a pull request to merge your code into the `master` branch.

## Resolving Conflicts
Merge conflicts occur when your branch diverts too much from the master branch. These can be resolved either automatically from the pull request in GitHub, or if it cannot resolve it for you, then you need to resolve it locally using the following commands:

1. Switch to the master branch and get the latest changes
```
git checkout master
git pull origin master
```
2. Go back to your branch, and merge the master branch into your branch to get your branch up to date
```
git checkout BRANCH_NAME
git merge origin master
```

3. Commit the changes and push the new updates to your branch
```
git commit
git push origin BRANCH_NAME
```
