#!/usr/bin/env bash

# set -o errexit
# config_file="updates-notifications.json"
# repos=`jq -r '.updates | map(.repository) | join("\n")' $config_file | tr -d '""'`

# function extract_folders {
#     jq -r ".updates | . [] | select(.repository == \"$1\") |.directories| join(\"\n\")" ../$config_file | tr -d '""'
# }

# function extract_reviewers {
#     jq -r ".updates | . [] | select(.repository == \"$1\") |.reviewers| join(\"\n\")" ../$config_file | tr -d '""'
# }

# function clone_and_process {
#     if [ ! -d "$repo" ]
#     then
#         git clone --depth 1 git@github.com:telerik/$repo
#     fi
#     cd $repo
#     folders=$(extract_folders $repo)
#     reviewers=$(extract_reviewers $repo)
#     echo "$reviewers"
#     for folder in $folders
#     do
#       cd ".$folder"
#       ncu -u --filter /^@progress/
#       create_pr $repo $reviewers
#     done
#     cd ..

# }

# function create_pr {
#     repo=$1
#     reviewers=$2
#     BRANCH_NAME="update-dependencies-$repo"
#     PRs=$(GITHUB_TOKEN=$TOKEN gh pr list | grep "$BRANCH_NAME" || true)
#     if [ ! -z $PRs ]; then
#         echo "Unmerged pr $BRANCH_NAME"
#     else
#         git fetch origin
#         git checkout -b $BRANCH_NAME
#         git config user.email "kendo-bot@progress.com"
#         git config user.name "kendo-bot"
#         git add package.json && git commit -m "chore: update dependencies"
#         git push -u origin $BRANCH_NAME
#         GITHUB_TOKEN=$TOKEN \
#         gh pr create --base master --head $BRANCH_NAME --reviewer $reviewers \
#         --title "Update dependencies $DATE" --body 'Please review and update dependencies'
#     fi
# }

# has_changes=0

# git diff --exit-code --quiet -- . || has_changes=1

# if [ $has_changes -eq 0  ]
# then
#     echo -e "${BOLD} No changes ${NC}"
# else
#     create_pr
# fi
# function cleanup() {
#     cd $repo
#     git checkout -- .
#     git clean -xdf
#     cd ..
# }

# function loop_over_repos() {
#     for item in $repos
#     do
#      repo=$item
#      "$@"
#    #  cleanup
#     done
# }


# loop_over_repos clone_and_process


function create_pr {
    reviewers="mparvanov"
    BRANCH_NAME="update-dependencies"
    PRs=$(GITHUB_TOKEN=$TOKEN gh pr list | grep "$BRANCH_NAME" || true)
    # if [ ! -z $PRs ]; then
    #     echo "Unmerged pr $BRANCH_NAME"
    # else
    #     git fetch origin
    #     git checkout -b $BRANCH_NAME
    #     git config user.email "kendo-bot@progress.com"
    #     git config user.name "kendo-bot"
    #     git add package.json && git commit -m "chore: update dependencies"
    #     git push -u origin $BRANCH_NAME
    #     GITHUB_TOKEN=$TOKEN \
    #     gh pr create --base master --head $BRANCH_NAME --reviewer $reviewers \
    #     --title "Update dependencies $DATE" --body 'Please review and update dependencies'
    fi
}

create_pr 