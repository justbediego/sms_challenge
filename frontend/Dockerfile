FROM node:alpine
WORKDIR /app

# install app dependencies
COPY package.json ./
COPY yarn.lock ./

RUN yarn install --silent
RUN yarn global add react-scripts --silent

COPY . .

EXPOSE 3000

# start app
CMD ["yarn", "start"]