#!/usr/bin/env bash
echo "test"
reviewers="mparvanov"
BRANCH_NAME="update-dependencies"
PRs=$(GITHUB_TOKEN=$TOKEN gh pr list | grep "$BRANCH_NAME" || true)
echo "PRs are:"
echo $PRs
echo "Branch is:"
echo $BRANCH_NAME
if [ ! -z $PRs ]; then
    echo "Unmerged pr $BRANCH_NAME"
else
    git fetch origin
    git checkout -b $BRANCH_NAME
    git config user.email "kendo-bot@progress.com"
    git config user.name "kendo-bot"
    git add package.json && git commit -m "chore: update dependencies"
    git push -u origin $BRANCH_NAME
    GITHUB_TOKEN=$TOKEN \
    gh pr create --base master --head $BRANCH_NAME --reviewer $reviewers \
    --title "Update dependencies $DATE" --body 'Please review and update dependencies'
fi
echo "PRs are:"

echo $PRs
